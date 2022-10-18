using ChatApp.Entities.Models;

namespace ChatApp.Contracts.Repositories
{
    public interface IConversationRepository
    {
        Task<IEnumerable<Conversation>> GetAllConversations(bool trackChanges);
        Task<IEnumerable<Conversation>> GetAllUserConversations(int userId, bool trackChanges);
        
        /// <summary>
        /// Grabs a specific conversation by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        Task<Conversation> GetConversation(int id, bool trackChanges);
        /// <summary>
        /// Helper method to check if a conversation between two parties already exists
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="recipient"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        Task<Conversation> GetConversation(string creator, string recipient, bool trackChanges);
        void CreateConversation(Conversation conversation);
    }
}
