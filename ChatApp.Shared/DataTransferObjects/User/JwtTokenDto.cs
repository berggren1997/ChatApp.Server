namespace ChatApp.Shared.DataTransferObjects.User
{
    public class JwtTokenDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
