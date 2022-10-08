using ChatApp.Shared.DataTransferObjects.ChatMessages;

namespace ChatApp.Shared.DataTransferObjects.Conversations
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string CreatedByAppUserName { get; set; } = string.Empty;
        public List<MessageDto> ChatMessages { get; set; } = new();
    }
}
