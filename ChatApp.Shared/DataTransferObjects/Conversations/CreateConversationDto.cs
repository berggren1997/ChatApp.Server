namespace ChatApp.Shared.DataTransferObjects.Conversations
{
    public class CreateConversationDto
    {
        public string RoomName { get; set; } = string.Empty;
        public int CreatedByAppUserId { get; set; }
    }
}
