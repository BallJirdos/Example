using DataLayerApi.Models.Entities.UserManagement;
using DataLayerApi.Models.Hateoas;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataLayerApi.Models.Dtos.V10.UserManagement
{
    /// <summary>
    /// Uživatel
    /// </summary>
    public class UserModel : IDtoIdentity
    {
        /// <summary>
        /// ID uživatele
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Uživatelské jméno
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "Length must be less then 50 characters")]
        public string UserName { get; set; }

        /// <summary>
        /// Je aktivní
        /// </summary>
        [DefaultValue(true)]
        public bool? IsEnabled { get; set; }

        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"
, ErrorMessage = "Email seems to be invalid. Please check the request.")]
        public string Email { get; set; }

        [RegularExpression(@"^\+(?:[0-9]?){6,14}[0-9]$|^[0-9]{8,14}$", ErrorMessage = "Phone number is not valid. Please check the request.")]
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
