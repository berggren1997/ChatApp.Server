namespace ChatApp.Entities.Exceptions.BadRequests
{
    public class ConversationBadRequestException : BadRequestException
    {
        public ConversationBadRequestException(string message) : base(message)
        {
        }
    }
}
