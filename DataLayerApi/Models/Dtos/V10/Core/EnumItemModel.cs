using DataLayerApi.Models.Attributes;
using DataLayerApi.Models.Hateoas;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayerApi.Models.Dtos.V10.Core
{
    /// <summary>
    /// Položka číselníku
    /// </summary>
    public class EnumItemModel : IDtoEnumItem
    {
        /// <summary>
        /// ID položky
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Kód položky číselníky
        /// </summary>
        [LinkParameter]
        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string NormalizedName { get; set; }

        /// <summary>
        /// Název položky číselníky
        /// </summary>
        [Required]
        [MaxLength(100, ErrorMessage = "Length must be less then 100 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Popis položky číselníky
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
        /// JSON nastavení
        /// </summary>
        public string Settings { get; set; }

        /// <summary>
        /// Typ položky
        /// </summary>
        [Required]
        public virtual Link EnumItemType { get; set; }
    }
}
