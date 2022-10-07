using ChatApp.Service.Contracts.Authentication;
using ChatApp.Service.Contracts.Message;

namespace ChatApp.Service.Contracts
{
    public interface IServiceManager
    {
        //public IChatMessageService ChatMessageService { get; }
        public IAuthService AuthService { get; }
    }
}
