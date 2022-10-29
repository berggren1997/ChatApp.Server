using ChatApp.Service.Contracts.Authentication;
using ChatApp.Service.Contracts.Conversations;
using ChatApp.Service.Contracts.GroupMessages;
using ChatApp.Service.Contracts.Groups;
using ChatApp.Service.Contracts.Message;

namespace ChatApp.Service.Contracts
{
    public interface IServiceManager
    {
        public IMessageService MessageService { get; }
        public IConversationService ConversationService { get; }
        public IGroupMessageService GroupMessageService { get; }
        public IGroupService GroupService { get; }
        public IAuthService AuthService { get; }
    }
}
