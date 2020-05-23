using DataLayerApi.Extensions;
using DataLayerApi.Models.Entities.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerApi.Configuration.EntityFramework.UserManagement
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasUniqueIndex(p => new { p.UserName });

            builder.HasMany(u => u.Logins).WithOne(pm => pm.User).HasForeignKey(pm => pm.UserId).IsRequired();
            
            builder.Property(u => u.IsEnabled).HasDefaultValue(true);
            builder.Property(u => u.FirstName).HasColumnType("NVARCHAR(150)").IsRequired();
            builder.Property(u => u.LastName).HasColumnType("NVARCHAR(150)").IsRequired();
        }
    }
}
