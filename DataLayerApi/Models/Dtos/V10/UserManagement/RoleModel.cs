using DataLayerApi.Models.Dtos.V10.Core;
using DataLayerApi.Models.Entities.UserManagement;
using DataLayerApi.Models.Hateoas;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataLayerApi.Models.Dtos.V10.UserManagement
{
    /// <summary>
    /// Uživatelská skupina
    /// </summary>
    public class RoleModel : IDtoEnumItem
    {
        /// <summary>
        /// ID uživatelské skupiny
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Kód uživatelské skupiny
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        [JsonIgnore]
        public string NormalizedName { get; set; }

        /// <summary>
        /// Role uživatele
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public ERoles Code
        {
            get { return this.ToEntityItemEnum<ERoles>(); }
            set { this.NormalizedName = value.ToString(); }
        }

        /// <summary>
        /// Název uživatelské skupiny
        /// </summary>
        [Required]
        [MaxLength(100, ErrorMessage = "Length must be less then 100 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Popis uživatelské skupiny
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
        /// Uživatelé ve skupině
        /// </summary>
        public ApiCollection<User> Users { get; set; }
    }
}
