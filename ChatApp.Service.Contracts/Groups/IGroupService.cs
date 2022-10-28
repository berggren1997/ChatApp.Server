using ChatApp.Shared.DataTransferObjects.Groups;

namespace ChatApp.Service.Contracts.Groups
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetGroups(bool trackChanges);
        Task<GroupDto> GetGroup(int id, bool trackChanges);
        Task CreateGroup(CreateGroupDto createGroupDto);
    }
}
