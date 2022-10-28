using ChatApp.Service.Contracts.Authentication;
using ChatApp.Service.Contracts.Conversations;
using ChatApp.Service.Contracts.Message;

namespace ChatApp.Service.Contracts
{
    public interface IServiceManager
    {
        public IMessageService ChatMessageService { get; }
        public IConversationService ConversationService { get; }
        public IAuthService AuthService { get; }
    }
}
