namespace ChatApp.Entities.Models
{
    public class GroupMessage
    {
        public int Id { get; set; }
        public string ChatMessage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public AppUser? Sender { get; set; }
        public Group? Group { get; set; }

    }
}
