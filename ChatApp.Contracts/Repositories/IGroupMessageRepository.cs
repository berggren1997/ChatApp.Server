using ChatApp.Entities.Models;

namespace ChatApp.Contracts.Repositories
{
    public interface IGroupMessageRepository
    {
        Task<IEnumerable<GroupMessage>> GetGroupMessages(int groupId, bool trackChanges);
        void CreateGroupMessage(GroupMessage groupMessage);
    }
}
