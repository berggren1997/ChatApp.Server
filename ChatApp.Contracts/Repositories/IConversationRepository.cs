using ChatApp.Entities.Models;

namespace ChatApp.Contracts.Repositories
{
    public interface IConversationRepository
    {
        Task<IEnumerable<Conversation>> GetAllConversations(bool trackChanges);
        Task<IEnumerable<Conversation>> GetAllUserConversations(int userId, bool trackChanges);
        Task<Conversation> GetConversation(int id, bool trackChanges);
        void CreateConversation(Conversation conversation);
    }
}
