using DataLayerApi.Models.Attributes;
using DataLayerApi.Models.Entities.Core;
using DataLayerApi.Models.Hateoas;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayerApi.Models.Dtos.V10.Core
{
    /// <summary>
    /// Typ číselníkové položky
    /// </summary>
    public class EnumItemTypeModel : IDtoEnumItem
    {
        /// <summary>
        /// ID číselníkové položky
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Kód číselníkové položky
        /// </summary>
        [LinkParameter]
        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string NormalizedName { get; set; }

        /// <summary>
        /// Název číselníkové položky
        /// </summary>
        [Required]
        [MaxLength(100, ErrorMessage = "Length must be less then 100 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Popis číselníkové položky
        /// </summary>
        [MaxLength(250, ErrorMessage = "Length must be less then 250 characters")]
        public string Title { get; set; }

        /// <summary>
        /// Je aktivní
        /// </summary>
        [Required]
        [DefaultValue(true)]
        public bool? IsEnabled { get; set; }

        /// <summary>
        /// Řazení
        /// </summary>
        [DefaultValue(0)]
        public int? Order { get; set; }

        /// <summary>
        /// Kód nadřazené položky
        /// </summary>
        [MaxLength(250, ErrorMessage = "Length must be less then 250 characters")]
        public string ParentCode { get; set; }

        /// <summary>
        /// Číselníkové položky
        /// </summary>
        public ApiCollection<EnumItem> EnumItems { get; set; }
    }
}
