using DataLayerApi.Extensions;
using DataLayerApi.Models.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayerApi.Configuration.EntityFramework.Core
{
    public class EnumItemConfiguration : IEntityTypeConfiguration<EnumItem>
    {
        public void Configure(EntityTypeBuilder<EnumItem> builder)
        {
            //indexy
            builder.HasUniqueIndex(e => new { e.IsEnabled, e.EnumItemTypeId, e.NormalizedName }).IncludeProperties(e => e.Name);

            //FK
            builder.HasOne(e => e.EnumItemType).WithMany(et => et.EnumItems).HasForeignKey(s => s.EnumItemTypeId);
            builder.HasMany(e => e.LoginTypes).WithOne(p => p.AccountType).HasForeignKey(s => s.EnumItemId_AccountType).OnDelete(DeleteBehavior.NoAction);
            
            builder.Property(g => g.IsEnabled).HasDefaultValue(true);
            //builder.Property<string>("Settings").HasField("_settings");

            EnumItemDataSeed seedBuilder = new EnumItemDataSeed();

            builder.HasData(seedBuilder.GetEntities());
        }
    }
}
