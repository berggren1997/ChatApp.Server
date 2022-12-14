namespace ChatApp.Entities.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string ChatMessage { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        
        public AppUser? Sender { get; set; }
        public int SenderId { get; set; }
        public Conversation? Conversation { get; set; }
        public int ConversationId { get; set; }
    }
}
