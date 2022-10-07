using ChatApp.Shared.DataTransferObjects.User;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Service.Contracts.Authentication
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(UserRegistrationDto createUserDto);
        Task<bool> ValidateUser(UserAuthenticationDto user); //checking login credentials
        Task<string> CreateToken();
    }
}
