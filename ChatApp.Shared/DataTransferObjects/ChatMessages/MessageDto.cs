namespace ChatApp.Shared.DataTransferObjects.ChatMessages
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string FromUsername { get; set; } = string.Empty;
    }
}
