namespace ChatApp.Entities.Exceptions.NotFoundRequests
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() : base("User was not found in the database.")
        { }

        public UserNotFoundException(string message) : base(message)
        {

        }
    }
}
