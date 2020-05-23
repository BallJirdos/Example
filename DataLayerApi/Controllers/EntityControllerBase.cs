using AutoMapper;
using DataLayerApi.Models.Dtos;
using DataLayerApi.Models.Entities;
using DataLayerApi.Repositories;
using DataLayerApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataLayerApi.Controllers
{
    public abstract class EntityControllerBase<E, D> : ControllerBase
        where E : class, IEntity
        where D : class, IDtoIdentity
    {
        protected readonly IMapper mapper;
        protected readonly IRepository<E> repository;

        protected EntityControllerBase(IMapper mapper, IRepository<E> repository, ILogger<EntityControllerBase<E, D>> logger)
            : base(logger)
        {
            using var stopWatch = new StopWatch(logger, LogLevel.Debug, $"Initialize controller of type {typeof(D).Name}");
            this.mapper = mapper;
            this.repository = repository;
        }

        /// <summary>
        /// Získat všechny položky
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public virtual ActionResult<List<D>> GetAll()
        {
            this.logger.LogDebug("Vrátit všechny entity.");
            return this.GetData().Select(e => this.mapper.Map<D>(e)).ToList();
        }

        /// <summary>
        /// Získat položku podle ID
        /// </summary>
        /// <param name="id">ID položky</param>
        /// <returns>Položka</returns>
        [HttpGet("id/{id}")]
        public virtual ActionResult<D> Get(int id)
        {
            this.logger.LogDebug($"Vrátit entitu dle ID {id}.");

            if (!this.repository.IsEntityExists(id))
                this.BadRequest();

            var entity = this.repository.GetById(id);
            return this.mapper.Map<D>(entity);
        }

        /// <summary>
        /// Přidat položku
        /// </summary>
        /// <param name="item">Položka</param>
        /// <returns>Id vytvořené položky</returns>
        [HttpPost("add")]
        public virtual ActionResult<int> Create(D item)
        {
            this.logger.LogDebug($"Vytvořit novou entitu.");

            E entity = this.OnCreateMap(item);
            return this.repository.Insert(entity);
        }

        /// <summary>
        /// Mapování při přidání položky
        /// </summary>
        /// <param name="item">DTO</param>
        /// <returns>Entita</returns>
        protected virtual E OnCreateMap(D item)
        {
            return this.mapper.Map<E>(item);
        }

        /// <summary>
        /// Update položky
        /// </summary>
        /// <param name="item">Položka</param>
        [HttpPut("update")]
        public virtual ActionResult Update(D item)
        {
            this.logger.LogDebug($"Upravit entitu dle ID {item.Id}.");

            if (!item.Id.HasValue)
                this.BadRequest();

            if (!this.repository.IsEntityExists(item.Id.Value))
                this.BadRequest();

            E entity = this.OnUpdateMap(item);
            this.repository.Update(entity);
            return this.Ok();
        }

        /// <summary>
        /// Mapování při updatu položky
        /// </summary>
        /// <param name="item">DTO</param>
        /// <returns>Entita</returns>
        protected virtual E OnUpdateMap(D item)
        {
            return this.mapper.Map<E>(item);
        }

        /// <summary>
        /// Smazat položku
        /// </summary>
        /// <param name="id">ID položky</param>
        [HttpDelete("delete/{id}")]
        public virtual ActionResult Delete(int id)
        {
            this.logger.LogDebug($"Smazat entitu dle ID {id}.");

            if (!this.repository.IsEntityExists(id))
                this.BadRequest();

            this.repository.Delete(id);
            return this.Ok();
        }

        protected virtual IQueryable<E> GetData(Expression<Func<E, bool>> predicate = null)
        {
            return this.repository.Get(predicate);
        }
    }
}
