using System.ComponentModel.DataAnnotations;

namespace DataLayerApi.Models.Dtos.V10.UserManagement
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
