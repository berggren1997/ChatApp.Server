using Microsoft.AspNetCore.Identity;

namespace ChatApp.Entities.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryDate { get; set; }
        public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    }
}
