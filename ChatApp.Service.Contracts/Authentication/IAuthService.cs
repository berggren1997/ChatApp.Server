using ChatApp.Entities.Models;
using ChatApp.Shared.DataTransferObjects.User;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Service.Contracts.Authentication
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(UserRegistrationDto createUserDto);
        Task<bool> ValidateUser(UserAuthenticationDto user); //checking login credentials
        Task<JwtTokenDto> CreateToken(bool updateRefreshToken);
        Task<JwtTokenDto> RefreshToken(string accessToken, string refreshToken);
        Task<AppUser> GetCurrentUser();
        Task<List<UserDto>> SearchForUserByUsername(string username);
    }
}
