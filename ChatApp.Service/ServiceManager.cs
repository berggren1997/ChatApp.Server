using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Models;
using ChatApp.Service.Authentication;
using ChatApp.Service.Contracts;
using ChatApp.Service.Contracts.Authentication;
using ChatApp.Service.Contracts.Conversations;
using ChatApp.Service.Contracts.GroupMessages;
using ChatApp.Service.Contracts.Groups;
using ChatApp.Service.Contracts.Message;
using ChatApp.Service.Conversations;
using ChatApp.Service.GroupMessages;
using ChatApp.Service.Groups;
using ChatApp.Service.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ChatApp.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IConversationService> _conversationService;
        private readonly Lazy<IMessageService> _chatMessageService;
        private readonly Lazy<IGroupService> _groupService;
        private readonly Lazy<IGroupMessageService> _groupMessageService;
        private readonly Lazy<IAuthService> _authService;

        public ServiceManager(UserManager<AppUser> userManager, IConfiguration configuration,
            IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _conversationService = new Lazy<IConversationService>(() =>
            new ConversationService(repository, userAccessor, userManager));
            
            _chatMessageService = new Lazy<IMessageService>(() =>
            new MessageService(repository, userManager, userAccessor));
            
            _authService = new Lazy<IAuthService>(() =>
            new AuthService(userManager, configuration, userAccessor));

            _groupService = new Lazy<IGroupService>(() =>
                new GroupService(repository, userAccessor, userManager));

            _groupMessageService = new Lazy<IGroupMessageService>(() =>
                new GroupMessageService(repository));
        }

        public IConversationService ConversationService => _conversationService.Value;
        public IMessageService MessageService => _chatMessageService.Value;
        public IGroupService GroupService => _groupService.Value;
        public IGroupMessageService GroupMessageService => _groupMessageService.Value;
        public IAuthService AuthService => _authService.Value;
    }
}
