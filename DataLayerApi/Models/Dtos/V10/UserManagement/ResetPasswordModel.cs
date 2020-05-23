using System.ComponentModel.DataAnnotations;

namespace DataLayerApi.Models.Dtos.V10.UserManagement
{
    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}
