namespace ChatApp.Entities.Exceptions.NotFoundRequests
{
    public class ConversationNotFoundException : NotFoundException
    {
        public ConversationNotFoundException() : base("Conversations not found.")
        { }

        public ConversationNotFoundException(int id) : base($"Conversation with id {id} not found.")
        { }
    }
}
