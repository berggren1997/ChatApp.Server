namespace ChatApp.Entities.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppUser? CreatedByAppUser { get; set; }
        public AppUser? Recipient { get; set; }
        public List<Message> Messages { get; set; } = new();
    }
}
