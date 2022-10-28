using ChatApp.Entities.Models;

namespace ChatApp.Contracts.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessages(int conversationId, bool trackChanges);
        void CreateMessage(Message message);
    }
}
