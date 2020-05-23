using DataLayerApi.Models.Dtos.V10.Core;
using DataLayerApi.Models.Entities.Core;
using DataLayerApi.Services.Data;
using System.Text.Json;

namespace DataLayerApi.Configuration.EntityFramework.Core
{
    public class EnumItemDataSeed : DataSeedEnumBuilder<EnumItem>
    {
        public EnumItemDataSeed()
        {
            //Typ účtu
            this.CreateEntityWithNewOrder(new EnumItem
            {
                EnumItemTypeId = 1,
                NormalizedName = "Internal",
                Name = "Interní účet",
                Title = "Účet registrovaný v systému"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 1,
                NormalizedName = "Google",
                Name = "Gmail účet",
                Title = "Účet Google"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 1,
                NormalizedName = "Facebook",
                Name = "Facebook účet",
                Title = "Účet na facebook"
            });

            //Jednotky
            this.CreateEntityWithNewOrder(new EnumItem
            {
                EnumItemTypeId = 2,
                NormalizedName = "ml",
                Name = "mililitr"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 2,
                NormalizedName = "l",
                Name = "litr"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 2,
                NormalizedName = "g",
                Name = "gram"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 2,
                NormalizedName = "kg",
                Name = "kilogram"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 2,
                NormalizedName = "GToL",
                Name = "g/l",
                Title = "gram na litr"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 2,
                NormalizedName = "Percent",
                Name = "%",
                Title = "Procenta"
            });

            //Datový typ
            this.CreateEntityWithNewOrder(new EnumItem
            {
                EnumItemTypeId = 3,
                NormalizedName = "String",
                Name = "String",
                Title = "Řetězec"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 3,
                NormalizedName = "Int",
                Name = "Integer",
                Title = "Celočíselný typ"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 3,
                NormalizedName = "Double",
                Name = "Double",
                Title = "Číslo s desetinnou čárkou"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 3,
                NormalizedName = "Bool",
                Name = "Boolean",
                Title = "Boolean"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 3,
                NormalizedName = "DateTime",
                Name = "DateTime",
                Title = "Datum a čas"
            });

            //Státy
            this.CreateEntityWithNewOrder(new EnumItem
            {
                EnumItemTypeId = 4,
                NormalizedName = "CZ",
                Name = "ČR",
                Title = "Česká republika"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 4,
                NormalizedName = "SVK",
                Name = "Slovensko",
                Title = "Slovenská republika"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 4,
                NormalizedName = "AU",
                Name = "Rakousko",
                Title = "Rakousko"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 4,
                NormalizedName = "DE",
                Name = "Německo",
                Title = "Německo"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 4,
                NormalizedName = "FR",
                Name = "Francie",
                Title = "Francie"
            });
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 4,
                NormalizedName = "CHI",
                Name = "Chile",
                Title = "Chile"
            });

            //Měna
            this.CreateEntity(new EnumItem
            {
                EnumItemTypeId = 5,
                NormalizedName = "CZK",
                Name = "Kč",
                Title = "Koruna česká"
            });
        }
    }
}
