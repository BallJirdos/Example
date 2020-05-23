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
    public class LoginConfiguration : IEntityTypeConfiguration<Login>
    {
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            builder.HasUniqueIndex(a => new { a.UserId, a.UserName, a.EnumItemId_AccountType });
        }
    }
}
