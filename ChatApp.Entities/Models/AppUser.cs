using Microsoft.AspNetCore.Identity;

namespace ChatApp.Entities.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

        // Relationships
        //public ICollection<Message> ChatMessagesFromUser { get; set; } = new List<Message>();
        //public ICollection<Message> ChatMessagesToUser { get; set; } = new List<Message>();
    }
}
