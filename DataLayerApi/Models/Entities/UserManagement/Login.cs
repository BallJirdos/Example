using DataLayerApi.Models.Attributes;
using DataLayerApi.Models.Entities.Core;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayerApi.Models.Entities.UserManagement
{
    [Table("Logins")]
    public class Login : IdentityUserLogin<int>, IEntity
    {
        //[Key]
        //[LinkParameter]
        //[Column("LoginId")]
        [NotMapped]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string UserName { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public override int UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        [ForeignKey(nameof(AccountType))]
        public int EnumItemId_AccountType { get; set; }

        public virtual EnumItem AccountType { get; set; }
    }
}
