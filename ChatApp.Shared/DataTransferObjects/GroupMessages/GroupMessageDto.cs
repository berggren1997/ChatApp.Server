namespace ChatApp.Shared.DataTransferObjects.GroupMessages
{
    public class GroupMessageDto
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string FromUser { get; set; } = string.Empty;
    }
}
