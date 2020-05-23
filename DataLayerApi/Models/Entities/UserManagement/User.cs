using DataLayerApi.Models.Attributes;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayerApi.Models.Entities.UserManagement
{
    [Table("Users")]
    public class User : IdentityUser<int>, IEntity
    {
        [Key]
        [LinkParameter]
        [Column("UserId")]
        public override int Id { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "Length must be less then 50 characters")]
        public override string UserName { get; set; }

        [Required]
        public bool IsEnabled { get; set; } = true;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Login> Logins { get; set; }
    }
}
