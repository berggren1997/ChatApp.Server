using System.ComponentModel.DataAnnotations;

namespace ChatApp.Shared.DataTransferObjects.User
{
    public class UserRegistrationDto
    {
        [Required]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
        
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
