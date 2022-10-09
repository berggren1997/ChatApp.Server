namespace ChatApp.Entities.Exceptions.NotFoundRequests
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        { }
    }
}
