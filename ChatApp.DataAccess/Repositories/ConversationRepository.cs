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

        public async Task<IEnumerable<Conversation>> GetAllUserConversations(int userId, bool trackChanges) =>
            await FindByCondition(x => x.CreatedByAppUser.Id == userId || x.Recipient.Id == userId, trackChanges)
            .Include(x => x.CreatedByAppUser)
            .Include(x => x.Recipient)
            .Include(x => x.Messages)
            .ToListAsync();

        public async Task<Conversation> GetConversation(int id, bool trackChanges) =>
            await FindByCondition(x => x.Id == id, trackChanges)
            .Include(x => x.CreatedByAppUser)
            .Include(x => x.Recipient)
            .Include(x => x.Messages)
            .FirstOrDefaultAsync();

        public async Task<Conversation> GetConversation(string creator, string recipient, bool trackChanges) =>
            await FindByCondition(x => x.CreatedByAppUser.UserName == creator &&
            x.Recipient.UserName == recipient, trackChanges)
            .Include(x => x.CreatedByAppUser)
            .Include(x => x.Recipient)
            .FirstOrDefaultAsync();
    }
}
