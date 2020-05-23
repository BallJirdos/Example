using DataLayerApi.Models.Entities.UserManagement;
using DataLayerApi.Services.Data;

namespace DataLayerApi.Configuration.EntityFramework.UserManagement
{
    public class RoleDataSeed : DataSeedEnumBuilder<Role>
    {
        public RoleDataSeed()
        {
            this.CreateEntity(new Role
            {
                NormalizedName = "AllUsers",
                Name = "Všichni uživatelé",
                Title = "Skupina všech uživatelů",
                IsEnabled = false,
                ConcurrencyStamp = "c4c084bb-cab3-40fe-a19e-9e12820ba302"
            });
            this.CreateEntity(new Role
            {
                NormalizedName = "Admin",
                Name = "Administrator",
                Title = "Administrátor- správa aplikace",
                ConcurrencyStamp = "8fe1ee6a-162f-429c-8fc2-932f8fcfbee1"
            });
            this.CreateEntity(new Role
            {
                NormalizedName = "Seller",
                Name = "Prodejce",
                Title = "Prodejce zboží",
                ConcurrencyStamp = "5b32df3e-bd8e-44ce-92a4-d9d0a8e4184f"
            });
            this.CreateEntity(new Role
            {
                NormalizedName = "Customer",
                Name = "Zákazník",
                Title = "Zákazník",
                ConcurrencyStamp = "866269ed-3cc5-408c-9011-3052fe69f59f"
            });
            this.CreateEntity(new Role
            {
                NormalizedName = "VerifiedCustomer",
                Name = "OvěřenýZákazník",
                Title = "Ověřený zákazník",
                ConcurrencyStamp = "6a194d45-c591-430e-aae1-e0349bc826b8"
            });
            this.CreateEntity(new Role
            {
                NormalizedName = "Guest",
                Name = "Host",
                Title = "Host aplikace s minimálními oprávněními",
                ConcurrencyStamp = "a95c2572-b097-4e34-8b72-317298f89f54"
            });
            this.CreateEntity(new Role
            {
                NormalizedName = "Supplier",
                Name = "Dodavatel",
                Title = "Dodavatel zboží",
                ConcurrencyStamp = "edb0b8ed-0ec8-4dd4-948f-4002e6c84d14"
            });
        }
    }
}
