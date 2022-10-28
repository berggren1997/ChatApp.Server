using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Models;
using ChatApp.Service.Contracts.Authentication;
using ChatApp.Service.Contracts.Groups;
using ChatApp.Shared.DataTransferObjects.GroupMessages;
using ChatApp.Shared.DataTransferObjects.Groups;
using ChatApp.Shared.DataTransferObjects.User;

namespace ChatApp.Service.Groups
{
    public class GroupService : IGroupService
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public GroupService(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        public Task CreateGroup(CreateGroupDto createGroupDto)
        {
            var senderUsername = _userAccessor.GetCurrentUserName();

            throw new NotImplementedException();
        }

        public async Task<GroupDto> GetGroup(int id, bool trackChanges)
        {
            var group = await _repository.GroupRepository.GetGroup(id, trackChanges);
            
            if (group == null) 
                throw new Exception($"Temporary exception in {nameof(GetGroup)} method");

            var groupToReturn = MapToGroupDto(group);

            return groupToReturn;
        }

        public Task<IEnumerable<GroupDto>> GetGroups(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        private GroupDto MapToGroupDto(Group group)
        {
            var groupToReturn = new GroupDto
            {
                Id = group.Id,
                CreatedAt = group.CreatedAt,
                CreatedBy = group.CreatedBy!.UserName,
                GroupMessages = group.GroupMessages!.Select(x => new GroupMessageDto
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    FromUser = x.Sender!.UserName,
                    Message = x.ChatMessage
                }).ToList(),
                Participants = group.Participants!.Select(x => new UserDto
                {
                    Id = x.Id,
                    Username = x.UserName
                }).ToList()
            };
            return groupToReturn;
        }
    }
}
