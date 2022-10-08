using ChatApp.Shared.DataTransferObjects.ChatMessages;

namespace ChatApp.Service.Contracts.Message
{
    public interface IChatMessageService
    {
        Task CreateChatMessage(int conversationId, CreateChatMessageDto chatMessage, bool trackChanges);
    }
}
