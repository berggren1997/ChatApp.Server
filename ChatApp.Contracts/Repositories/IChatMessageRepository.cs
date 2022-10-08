using ChatApp.Entities.Models;

namespace ChatApp.Contracts.Repositories
{
    public interface IChatMessageRepository
    {
        Task<IEnumerable<Message>> GetMessages(int conversationId, bool trackChanges);
        void CreateMessage(Message message);
    }
}
