using ChatApp.Shared.DataTransferObjects.Conversations;

namespace ChatApp.Service.Contracts.Conversations
{
    public interface IConversationService
    {
        Task<ConversationDto> GetConversation(int id, bool trackChanges);
        Task<List<ConversationDto>> GetAllUserConversations(bool trackChanges);
        Task CreateConversation(string recipientUsername);
    }
}
