using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Models;
using ChatApp.Service.Authentication;
using ChatApp.Service.Contracts;
using ChatApp.Service.Contracts.Authentication;
using ChatApp.Service.Contracts.Conversations;
using ChatApp.Service.Contracts.Message;
using ChatApp.Service.Conversations;
using ChatApp.Service.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ChatApp.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IConversationService> _conversationService;
        private readonly Lazy<IChatMessageService> _chatMessageService;
        private readonly Lazy<IAuthService> _authService;

        public ServiceManager(UserManager<AppUser> userManager, IConfiguration configuration,
            IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _conversationService = new Lazy<IConversationService>(() =>
            new ConversationService(repository, userAccessor, userManager));
            
            _chatMessageService = new Lazy<IChatMessageService>(() =>
            new ChatMessageService(repository, userManager, userAccessor));
            
            _authService = new Lazy<IAuthService>(() =>
            new AuthService(userManager, configuration, userAccessor));   
        }

        public IConversationService ConversationService => _conversationService.Value;
        public IChatMessageService ChatMessageService => _chatMessageService.Value;
        public IAuthService AuthService => _authService.Value;
    }
}
