namespace ChatApp.Entities.Exceptions.BadRequests
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { }
    }
}
