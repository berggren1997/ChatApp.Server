using ChatApp.Entities.Models;
using ChatApp.Service.Authentication;
using ChatApp.Service.Contracts;
using ChatApp.Service.Contracts.Authentication;
using ChatApp.Service.Contracts.Message;
using ChatApp.Service.Message;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ChatApp.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthService> _authService;
        //private readonly Lazy<IChatMessageService> _chatMessageService;
        
        public ServiceManager(UserManager<AppUser> userManager, IConfiguration configuration)

        {
            _authService = new Lazy<IAuthService>(() =>
            new AuthService(userManager, configuration));
            //_chatMessageService = new Lazy<IChatMessageService>(() =>
            //new ChatMessageService());
        }

        public IAuthService AuthService => _authService.Value;
        //public IChatMessageService ChatMessageService => _chatMessageService.Value;
    }
}
