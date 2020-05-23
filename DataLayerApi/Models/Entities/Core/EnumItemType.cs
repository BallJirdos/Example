using DataLayerApi.Models.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataLayerApi.Models.Entities.Core
{
    [Table("EnumItemTypes")]
    public class EnumItemType : IEntityEnumItem
    {
        [Key]
        [Column("EnumItemTypeId")]
        public int Id { get; set; }

        [LinkParameter]
        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string NormalizedName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Length must be less then 100 characters")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Length must be less then 250 characters")]
        public string Title { get; set; }

        [Required]
        public bool IsEnabled { get; set; } = true;

        public int Order { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual EnumItemType Parent { get; set; }

        public virtual ICollection<EnumItemType> Children { get; set; }

        public virtual ICollection<EnumItem> EnumItems { get; set; }
    }
}
