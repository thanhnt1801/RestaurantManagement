using System.ComponentModel.DataAnnotations;

namespace QLNH_Client.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters, dude!")]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public int RestaurantId { get; set; }
    }
}
