namespace ChatApp.Shared.DataTransferObjects.Conversations
{
    public class CreateConversationDto
    {
        public string RecipientUsername { get; set; } = string.Empty;
        public string? StartMessage { get; set; }
    }
}
