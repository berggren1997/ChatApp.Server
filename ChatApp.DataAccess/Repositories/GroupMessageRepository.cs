using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Repositories
{
    public class GroupMessageRepository : RepositoryBase<GroupMessage>, IGroupMessageRepository
    {
        public GroupMessageRepository(AppDbContext context) : base(context)
        { }

        public void CreateGroupMessage(GroupMessage groupMessage) =>
            Create(groupMessage);

        public async Task<IEnumerable<GroupMessage>> GetGroupMessages(int groupId, bool trackChanges) =>
            await FindByCondition(x => x.Group.Id == groupId, trackChanges)
            .ToListAsync();
    }
}
