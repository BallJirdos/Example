using AutoMapper;
using DataLayerApi.Exceptions;
using DataLayerApi.Models.Dtos.V10.Core;
using DataLayerApi.Models.Entities.Core;
using DataLayerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataLayerApi.Controllers.Core
{
    /// <summary>
    /// Číselníkové typy
    /// </summary>
    [Route("api/core/enumType")]
    public class EnumItemTypeController : EnumItemControllerBase<EnumItemType, EnumItemTypeModel>
    {
        private readonly UnitOfWorkFactory unitOfWorkFactory;

        public EnumItemTypeController(IMapper mapper, 
            IRepository<EnumItemType> repository, 
            ILogger<EnumItemTypeController> logger,
            UnitOfWorkFactory unitOfWorkFactory)
            : base(mapper, repository, logger)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        protected override IQueryable<EnumItemType> GetData(Expression<Func<EnumItemType, bool>> predicate = null)
        {
            return base.GetData(predicate)
                .Include(e => e.EnumItems)
                .Include(e => e.Parent);
        }

        /// <summary>
        /// Získat všechny položky
        /// </summary>
        /// <returns></returns>
        [HttpGet("allTypes")]
        [ResponseCache(Duration = 300)]
        public override ActionResult<List<EnumItemTypeModel>> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// Získat všechny položky
        /// </summary>
        /// <returns></returns>
        [HttpGet("{parentNormalizedName}/children")]
        [ResponseCache(Duration = 300)]
        public ActionResult<List<EnumItemTypeModel>> GetTypesByParent(string parentNormalizedName)
        {
            return this.GetData()
                .Where(ei => ei.Parent.NormalizedName == parentNormalizedName)
                .OrderBy(ei => ei.Order)
                .Select(ei => this.mapper.Map<EnumItemTypeModel>(ei)).ToList();
        }

        /// <summary>
        /// Získat všechny položky dle typu
        /// </summary>
        /// <param name="typeCode">Kód typu položky</param>
        /// <param name="showOnlyActive">Zobrazit pouze aktivní</param>
        /// <returns>Kolekce položek</returns>
        [ResponseCache(Duration = 300)]
        [HttpGet("{typeCode}/item/all")]
        public ActionResult<IEnumerable<EnumItemModel>> GetAllItem([Required]string typeCode, [FromQuery] bool showOnlyActive = true)
        {
            var enumItemType = this.GetData(eit => eit.NormalizedName == typeCode && (showOnlyActive == eit.IsEnabled || !showOnlyActive))
                .FirstOrDefault();
            if (enumItemType == null)
                throw new HttpResponseException($"EnumItemType with code {typeCode} not exists.", HttpStatusCode.BadRequest);

            var result = enumItemType.EnumItems.Select(ei => this.mapper.Map<EnumItemModel>(ei));
            return this.Ok(result);
        }

        /// <summary>
        /// Získat položku dle kódu
        /// </summary>
        /// <param name="typeCode">Kód typu položky</param>
        /// <param name="itemCode">Kód položky</param>
        /// <returns>Položka</returns>
        [ResponseCache(Duration = 300)]
        [HttpGet("{typeCode}/item/{itemCode}")]
        public ActionResult<EnumItemModel> GetEnumItem([Required]string typeCode, [Required]string itemCode)
        {
            var enumItemType = this.GetData(eit => eit.NormalizedName == typeCode)
                .FirstOrDefault();
            if (enumItemType == null)
                throw new HttpResponseException($"EnumItemType with code {typeCode} not exists.", HttpStatusCode.BadRequest);

            var enumItem = enumItemType.EnumItems.FirstOrDefault(e => e.NormalizedName == itemCode);
            if (enumItem == null)
                throw new HttpResponseException($"EnumItem for typeCode {typeCode} wit ItemCode {itemCode} not exists.", HttpStatusCode.BadRequest);

            return this.mapper.Map<EnumItemModel>(enumItem);
        }

        /// <summary>
        /// Přidat položku
        /// </summary>
        /// <param name="typeCode">Kód typu položky</param>
        /// <param name="itemModel">Model položky</param>
        /// <returns>Položka</returns>
        [HttpPost("{typeCode}/item/add")]
        public ActionResult<int> CreateEnumItem([Required]string typeCode, [Required]EnumItemModel itemModel)
        {
            var enumItemType = this.GetData(eit => eit.NormalizedName == typeCode)
                .FirstOrDefault();
            if (enumItemType == null)
                throw new HttpResponseException($"EnumItemType with code {typeCode} not exists.", HttpStatusCode.BadRequest);

            using var uow = this.unitOfWorkFactory.Create();
            var entity = this.mapper.Map<EnumItem>(itemModel);
            entity.EnumItemTypeId = enumItemType.Id;
            var enumItemRepository = uow.GetRepository<EnumItem>();

            uow.BeginTransaction();
            enumItemRepository.Insert(entity);

            uow.Save();

            return entity.Id;
        }

        /// <summary>
        /// Přidat položku
        /// </summary>
        /// <param name="itemId">ID položky</param>
        /// <param name="itemModel">Model položky</param>
        /// <returns>Položka</returns>
        [HttpPut("item/{itemId}/update")]
        public ActionResult<int> UpdateEnumItem([Required]int itemId, [Required]EnumItemModel itemModel)
        {
            using var uow = this.unitOfWorkFactory.Create();
            var enumItemRepository = uow.GetRepository<EnumItem>();
            var enumItem = enumItemRepository.GetById(itemId);
            if (enumItem == null)
                throw new HttpResponseException($"EnumItem with id {itemId} not exists.", HttpStatusCode.BadRequest);

            var entity = this.mapper.Map<EnumItem>(itemModel);
            entity.EnumItemTypeId = enumItem.EnumItemTypeId;
            entity.Id = itemId;

            uow.BeginTransaction();
            enumItemRepository.Update(entity);

            uow.Save();
            return entity.Id;
        }

        [NonAction]
        public override ActionResult Delete(int id)
        {
            return base.Delete(id);
        }

        [NonAction]
        public override ActionResult DeleteByCode([Required] string code)
        {
            return base.DeleteByCode(code);
        }

        protected override EnumItemType OnCreateMap(EnumItemTypeModel item)
        {
            return this.FillParent(item);
        }

        protected override EnumItemType OnUpdateMap(EnumItemTypeModel item)
        {
            return this.FillParent(item);
        }

        private EnumItemType FillParent(EnumItemTypeModel item)
        {
            var ret = this.mapper.Map<EnumItemType>(item);
            if (!string.IsNullOrEmpty(item.ParentCode))
            {
                var parent = this.repository.GetByCode(item.ParentCode);
                ret.ParentId = parent?.Id;
            }
            return ret;
        }
    }
}
