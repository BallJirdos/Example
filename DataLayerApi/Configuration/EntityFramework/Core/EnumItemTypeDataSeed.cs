using DataLayerApi.Models.Entities.Core;
using DataLayerApi.Services.Data;
using System.Linq;

namespace DataLayerApi.Configuration.EntityFramework.Core
{
    public class EnumItemTypeDataSeed : DataSeedEnumBuilder<EnumItemType>
    {
        private readonly EnumItemDataSeed enumItemData = new EnumItemDataSeed();

        public EnumItemTypeDataSeed(bool createConstraint = false)
        {
            this.CreateEntity(new EnumItemType
            {
                Id = 1,
                NormalizedName = "AccountType",
                Name = "Typ účtu",
                Title = "Typ účtu k přihlášení uživatele"
            });
            this.CreateEntity(new EnumItemType
            {
                Id = 2,
                NormalizedName = "Unit",
                Name = "Jednotky"
            });
            this.CreateEntity(new EnumItemType
            {
                Id = 3,
                NormalizedName = "DataType",
                Name = "Datový typ",
                Title = "Datový typ"
            });
            this.CreateEntity(new EnumItemType
            {
                Id = 4,
                NormalizedName = "Country",
                Name = "Státy",
                Title = "Státy",
                ParentId = 1
            });
            this.CreateEntity(new EnumItemType
            {
                Id = 5,
                NormalizedName = "Currency",
                Name = "Měna"
            });

            if (createConstraint)
                this.FillEnumItem();
        }

        private void FillEnumItem()
        {
            var enumItems = this.enumItemData.GetEntities();
            foreach (var item in this.GetEntities())
            {
                item.EnumItems = enumItems.Where(ei => ei.EnumItemTypeId == item.Id).ToList();
            }
        }
    }
}
