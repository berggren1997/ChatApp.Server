using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Repositories
{
    public class ConversationRepository : RepositoryBase<Conversation>, IConversationRepository
    {
        public ConversationRepository(AppDbContext context) : base(context)
        { }

        public void CreateConversation(Conversation conversation) =>
            Create(conversation);

        public async Task<IEnumerable<Conversation>> GetAllConversations(bool trackChanges) =>
            await FindAll(trackChanges)
            .Include(x => x.Messages)
            .Include(x => x.CreatedByAppUser)
            .ToListAsync();

        public async Task<Conversation> GetConversation(int id, bool trackChanges) =>
            await FindByCondition(x => x.Id == id, trackChanges)
            .Include(x => x.CreatedByAppUser)
            .Include(x => x.Recipient)
            .Include(x => x.Messages)
            .FirstOrDefaultAsync();
    }
}
