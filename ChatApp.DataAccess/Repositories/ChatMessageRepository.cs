using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Repositories
{
    public class ChatMessageRepository : RepositoryBase<Message>, IChatMessageRepository
    {

        public ChatMessageRepository(AppDbContext context) : base(context)
        { }

        public void CreateMessage(Message message)
        {
            Create(message);
        }

        public async Task<IEnumerable<Message>> GetConversation(int conversationId)
        {
            return await FindByCondition(h => (h.SenderId == conversationId || 
            h.ReceiverId == conversationId), trackChanges: true)
                .OrderBy(x => x.CreatedAt)
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .ToListAsync();
        }
    }
}
