namespace ChatApp.Entities.Exceptions.NotFoundRequests
{
    public class MessagesNotFoundException : NotFoundException
    {
        public MessagesNotFoundException(string message) : base(message)
        {
        }
    }
}
