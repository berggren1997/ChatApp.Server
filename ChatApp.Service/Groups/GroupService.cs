using ChatApp.Contracts.Repositories;
using ChatApp.Entities.Exceptions.NotFoundRequests;
using ChatApp.Entities.Models;
using ChatApp.Service.Contracts.Authentication;
using ChatApp.Service.Contracts.Groups;
using ChatApp.Shared.DataTransferObjects.GroupMessages;
using ChatApp.Shared.DataTransferObjects.Groups;
using ChatApp.Shared.DataTransferObjects.User;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Service.Groups
{
    public class GroupService : IGroupService
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;
        private readonly UserManager<AppUser> _userManager;

        public GroupService(IRepositoryManager repository, IUserAccessor userAccessor,
            UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userAccessor = userAccessor;
            _userManager = userManager;
        }

        public async Task CreateGroup(CreateGroupDto createGroupDto)
        {
            var senderUsername = _userAccessor.GetCurrentUserName();

            var creator = await _userManager.FindByNameAsync(senderUsername);

            if (creator == null)
                throw new UserNotFoundException();
            //TODO: inte alls efficient, kolla upp hur fan man hämtar ut en lista som matchar en input lista
            //av usernames
            var participants = new List<AppUser>();

            foreach (var participant in createGroupDto.Participants)
            {
                var user = await _userManager.FindByNameAsync(participant.Username);
                if (user != null)
                {
                    participants.Add(user);
                }
            }
            var newGroup = new Group
            {
                CreatedAt = DateTime.Now,
                CreatedBy = creator,
                GroupMessages = new(),
                Participants = participants
            };
            _repository.GroupRepository.CreateGroup(newGroup);
            await _repository.SaveAsync();
        }

        public async Task<GroupDto> GetGroup(int id, bool trackChanges)
        {
            var group = await _repository.GroupRepository.GetGroup(id, trackChanges);

            if (group == null)
                throw new Exception($"Temporary exception in {nameof(GetGroup)} method");

            var groupToReturn = MapToGroupDto(group);

            return groupToReturn;
        }

        public async Task<IEnumerable<GroupDto>> GetGroups(bool trackChanges)
        {
            var groups = await _repository.GroupRepository.GetGroups(trackChanges);
            
            if(groups == null) 
                throw new Exception($"Temporary exception in {nameof(GetGroups)} method");
            
            var groupsToReturn = MapToGroupsDto(groups);
            
            return groupsToReturn;
        }

        private IEnumerable<GroupDto> MapToGroupsDto(IEnumerable<Group> groups)
        {
            return groups.Select(x => new GroupDto
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy.UserName,
                GroupMessages = x.GroupMessages.Select(x => new GroupMessageDto
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    FromUser = x.Sender.UserName,
                    Message = x.ChatMessage
                }).ToList(),
                Participants = x.Participants.Select(x => new UserDto
                {
                    Id = x.Id,
                    Username = x.UserName
                }).ToList()
            });
        }

        private GroupDto MapToGroupDto(Group group)
        {
            var groupToReturn = new GroupDto
            {
                Id = group.Id,
                CreatedAt = group.CreatedAt,
                CreatedBy = group.CreatedBy!.UserName,
                GroupMessages = group.GroupMessages.Select(x => new GroupMessageDto
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    FromUser = x.Sender!.UserName,
                    Message = x.ChatMessage
                }).ToList(),
                Participants = group.Participants.Select(x => new UserDto
                {
                    Id = x.Id,
                    Username = x.UserName
                }).ToList()
            };
            return groupToReturn;
        }
    }
}
