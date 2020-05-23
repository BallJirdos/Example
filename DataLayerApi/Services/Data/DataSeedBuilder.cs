using DataLayerApi.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataLayerApi.Services.Data
{
    public class DataSeedBuilder<E> where E : IEntity, new()
    {
        private int id = 1;
        protected IDictionary<int, E> idCache = new Dictionary<int, E>();

        /// <summary>
        /// Vytvořit entitu nastavením ID a Order
        /// </summary>
        /// <param name="entity">Entita</param>
        /// <returns>Entita</returns>
        public virtual E CreateEntity(E entity)
        {
            this.SetId(entity);

            this.AddEntityToCache(entity);
            return entity;
        }

        /// <summary>
        /// Získat kolekci entit
        /// </summary>
        /// <returns>Kolekce entit</returns>
        public E[] GetEntities()
        {
            return this.idCache.Values.ToArray();
        }

        protected virtual void SetId(E entity)
        {
            var id = entity.Id;
            var newId = id == 0 ? this.id++ : id;
            entity.Id = newId;
        }

        protected virtual void AddEntityToCache(E entity)
        {
            this.idCache.Add(entity.Id, entity);
        }
    }
}
