using ChatApp.Shared.DataTransferObjects.GroupMessages;

namespace ChatApp.Service.Contracts.GroupMessages
{
    public interface IGroupMessageService
    {
        Task<IEnumerable<GroupMessageDto>> GetGroupMessages(int groupId, bool trackChanges);
        Task<IEnumerable<GroupMessageDto>> GetAllGroupMessages(bool trackChanges);
        Task CreateGroupMessage(GroupMessageDto groupMessage);
    }
}
