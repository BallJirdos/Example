using DataLayerApi.Models.Attributes;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayerApi.Models.Entities.UserManagement
{
    [Table("Roles")]
    public class Role :IdentityRole<int>, IEntityEnumItem
    {
        [Key]
        [Column("RoleId")]
        public override int Id { get; set; }

        [LinkParameter]
        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public override string NormalizedName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Length must be less then 100 characters")]
        public override string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Length must be less then 250 characters")]
        public string Title { get; set; }

        [Required]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Řazení
        /// </summary>
        [DefaultValue(0)]
        public int Order { get; set; }
    }
}
