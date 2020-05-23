using DataLayerApi.Models.Attributes;
using DataLayerApi.Models.Entities.UserManagement;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DataLayerApi.Models.Entities.Core
{
    [Table("EnumItems")]
    public class EnumItem : IEntityEnumItem
    {
        [Key]
        [Column("EnumItemId")]
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

        /// <summary>
        /// Řazení
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// JSON settings
        /// </summary>
        public string Settings { get; set; }

        [NotMapped]
        public JsonDocument JsonSettings
        {
            get
            {
                return JsonDocument.Parse(string.IsNullOrEmpty(this.Settings) ? "{}" : this.Settings);
            }
            set
            {
                using (var stream = new MemoryStream())
                {
                    Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true });
                    value.WriteTo(writer);
                    writer.Flush();
                    this.Settings = Encoding.UTF8.GetString(stream.ToArray());
                }
            }
        }

        public int EnumItemTypeId { get; set; }

        [ForeignKey(nameof(EnumItemTypeId))]
        public virtual EnumItemType EnumItemType { get; set; }

        public virtual ICollection<Login> LoginTypes { get; set; }
    }
}
