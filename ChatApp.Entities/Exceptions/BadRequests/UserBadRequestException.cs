namespace ChatApp.Entities.Exceptions.BadRequests
{
    public class UserBadRequestException : BadRequestException
    {
        public UserBadRequestException(string message) : base(message)
        { }
    }
}
