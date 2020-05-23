using DataLayerApi.Models.Hateoas;
using System.ComponentModel.DataAnnotations;

namespace DataLayerApi.Models.Dtos.V10.UserManagement
{
    /// <summary>
    /// Uživatelský účet
    /// </summary>
    public class AccountModel : IDtoIdentity
    {
        /// <summary>
        /// ID účtu
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Uživatelské jméno
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string UserName { get; set; }

        /// <summary>
        /// Uživatel
        /// </summary>
        [Required]
        public Link User { get; set; }

        /// <summary>
        /// Typ účtu
        /// </summary>
        [Required]
        public Link AccountType { get; set; }
    }
}
