using ChatApp.Entities.Models;

namespace ChatApp.Contracts.Repositories
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetGroups(bool trackChanges);
        Task<Group> GetGroup(int groupId, bool trackChanges);
        void CreateGroup(Group group);
    }
}
