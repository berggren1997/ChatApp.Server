namespace ChatApp.Entities.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string ChatMessage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
        // Relationships
        public AppUser? Sender { get; set; }
        public int SenderId { get; set; }
        public AppUser? Receiver { get; set; }
        public int ReceiverId { get; set; }
    }
}
