namespace ChatApp.Service.Contracts.Authentication
{
    public interface IUserAccessor
    {
        string? GetCurrentUserName();
        int GetCurrentUserId();
    }
}
