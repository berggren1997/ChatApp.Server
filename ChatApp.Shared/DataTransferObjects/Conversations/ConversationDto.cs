using ChatApp.Shared.DataTransferObjects.ChatMessages;

namespace ChatApp.Shared.DataTransferObjects.Conversations
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;
        public List<MessageDto> ChatMessages { get; set; } = new();
    }
}
