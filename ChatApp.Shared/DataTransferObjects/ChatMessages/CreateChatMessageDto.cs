namespace ChatApp.Shared.DataTransferObjects.ChatMessages
{
    public class CreateChatMessageDto
    {
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
