namespace ChatApp.Shared.DataTransferObjects.ChatMessages
{
    public class ChatMessagesDto
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public List<MessageDto> ChatMessages { get; set; } = new();
    }
}
