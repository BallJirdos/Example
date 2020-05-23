using DataLayerApi.Extensions;
using DataLayerApi.Models.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace DataLayerApi.Configuration.EntityFramework.UserManagement
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasDefaultUniqueIndex();
            builder.HasBasicIndex(g => new { g.IsEnabled, g.NormalizedName })
                .IncludeProperties(g => new { g.Name, g.Title });

            builder.Property(g => g.IsEnabled).HasDefaultValue(true);

            RoleDataSeed seedBuilder = new RoleDataSeed();
            builder.HasData(seedBuilder.GetEntities());
        }
    }
}
