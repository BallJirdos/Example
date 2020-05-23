using DataLayerApi.Extensions;
using DataLayerApi.Models.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayerApi.Configuration.EntityFramework.Core
{
    public class EnumItemTypeConfiguration : IEntityTypeConfiguration<EnumItemType>
    {
        public void Configure(EntityTypeBuilder<EnumItemType> builder)
        {
            builder.HasDefaultUniqueIndex(); ;

            builder.Property(g => g.IsEnabled).HasDefaultValue(true);

            builder.HasOne(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentId);

            EnumItemTypeDataSeed dataSeed = new EnumItemTypeDataSeed();

            builder.HasData(dataSeed.GetEntities());
        }
    }
}
