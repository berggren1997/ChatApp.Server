using ChatApp.Entities.Models;

namespace ChatApp.Contracts.Repositories
{
    public interface IChatMessageRepository
    {
        Task<IEnumerable<Message>> GetConversation(int conversationId);
        void CreateMessage(Message message);
    }
}
