using AutoMapper;
using DataLayerApi.Models.Dtos;
using DataLayerApi.Models.Entities;
using DataLayerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace DataLayerApi.Controllers
{
    public abstract class EnumItemControllerBase<E, D> : EntityControllerBase<E, D>
        where E : class, IEntityEnumItem
        where D : class, IDtoEnumItem
    {
        protected EnumItemControllerBase(IMapper mapper, IRepository<E> repository, ILogger<EnumItemControllerBase<E, D>> logger)
            : base(mapper, repository, logger)
        {
        }

        //[NonAction]
        //public override ActionResult<D> Get(int id)
        //{
        //    return base.Get(id);
        //}

        /// <summary>
        /// Získat položku podle kódu
        /// </summary>
        /// <param name="code">Kód položky</param>
        /// <returns>Položka</returns>
        [HttpGet("code/{code}")]
        public virtual ActionResult<D> GetByCode(string code)
        {
            this.logger.LogDebug($"Získat entitu dle kódu {code}.");

            return this.mapper.Map<D>(this.repository.GetByCode(code));
        }

        /// <summary>
        /// Smazat položku podle ID
        /// </summary>
        /// <param name="id">ID položky</param>
        [HttpDelete("deleteById{id}")]
        public override ActionResult Delete(int id)
        {
            this.logger.LogDebug($"Smazat entitu dle ID {id}.");

            var code = this.repository.GetById(id)?.NormalizedName;
            if (code == null)
            {
                this.BadRequest();
            }
            this.repository.DeleteItem(t => t.NormalizedName == code);
            return this.Ok();
        }

        /// <summary>
        /// Smazat položku podle kódu
        /// </summary>
        /// <param name="code">Kód položky</param>
        /// <returns></returns>
        [HttpDelete("deleteByCode/{typeCode}")]
        public virtual ActionResult DeleteByCode([Required]string code)
        {
            this.logger.LogDebug($"Smazat entitu podle kódu {code}.");

            this.repository.DeleteItem(t => t.NormalizedName == code);
            return this.Ok();
        }
    }
}
