using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Repositories
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext context) : base(context)
        { }

        public void CreateGroup(Group group) =>
            Create(group);

        public async Task<Group> GetGroup(int groupId, bool trackChanges) =>
            await FindByCondition(x => x.Id == groupId, trackChanges)
            .Include(x => x.CreatedBy)
            .Include(x => x.Participants)
            .Include(x => x.GroupMessages)
            .FirstOrDefaultAsync();

        /// <summary>
        /// AsSplitQuery to increase performance according to EF log warn
        /// </summary>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Group>> GetGroups(bool trackChanges) =>
            await FindAll(trackChanges)
            .AsSplitQuery()
            .Include(x => x.CreatedBy)
            .Include(x => x.Participants)
            .Include(x => x.GroupMessages)
            .ToListAsync();
    }
}
