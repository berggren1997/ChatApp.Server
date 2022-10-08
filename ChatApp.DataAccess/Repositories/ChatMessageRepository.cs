using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Repositories
{
    public class ChatMessageRepository : RepositoryBase<Message>, IChatMessageRepository
    {

        public ChatMessageRepository(AppDbContext context) : base(context)
        { }

        public async Task<IEnumerable<Message>> GetMessages(int conversationId, bool trackChanges) =>
            await FindByCondition(x => x.ConversationId == conversationId, trackChanges)
            .Include(x => x.Sender)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
        public void CreateMessage(Message message) =>
            Create(message);
    }
}
