using ChatApp.Contracts.Repositories;
using ChatApp.Service.Contracts.GroupMessages;
using ChatApp.Shared.DataTransferObjects.GroupMessages;

namespace ChatApp.Service.GroupMessages
{
    public class GroupMessageService : IGroupMessageService
    {
        private readonly IRepositoryManager _repository;

        public GroupMessageService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public Task CreateGroupMessage(GroupMessageDto groupMessage)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupMessageDto>> GetAllGroupMessages(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupMessageDto>> GetGroupMessages(int groupId, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
