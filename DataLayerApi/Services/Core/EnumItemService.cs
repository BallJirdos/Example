using DataLayerApi.Models.Dtos.V10.Core;
using DataLayerApi.Models.Entities.Core;
using DataLayerApi.Repositories;
using System;

namespace DataLayerApi.Services.Core
{
    public class EnumItemService
    {
        private readonly IRepository<EnumItem> enumItemRepository;

        public EnumItemService(IRepository<EnumItem> enumItemRepository)
        {
            this.enumItemRepository = enumItemRepository;
        }

        /// <summary>
        /// Získat entitu dle enumu
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public EnumItem GetEnumItem(Enum @enum)
        {
            var enumItemId = @enum.ToEntityId();
            return this.enumItemRepository.GetById(enumItemId);
        }
    }
}
