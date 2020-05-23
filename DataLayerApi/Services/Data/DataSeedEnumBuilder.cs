using DataLayerApi.Models.Entities;

namespace DataLayerApi.Services.Data
{
    public class DataSeedEnumBuilder<E>: DataSeedBuilder<E> where E : IEntityEnumItem, new()
    {
        private int order = 0;

        /// <summary>
        /// Vytvořit entitu nastavením ID a Order
        /// </summary>
        /// <param name="entity">Entita</param>
        /// <returns>Entita</returns>
        public override E CreateEntity(E entity)
        {
            this.SetOrder(entity);

            return base.CreateEntity(entity);
        }

        /// <summary>
        /// Vytvořit entitu nastavením ID a pokud je Order != 0, tak se nastaví globálně na novou hodnotu
        /// </summary>
        /// <param name="entity">Entita</param>
        /// <returns>Entita</returns>
        public E CreateEntityWithNewOrder(E entity)
        {
            this.order = entity.Order == 0 ? 0 : entity.Order;
            
            return this.CreateEntity(entity);
        }

        protected virtual void SetOrder(E entity)
        {
            var order = entity.Order;
            var newOrder = order == 0 ? (this.order += 100) : order;
            entity.Order = newOrder;
        }
    }
}
