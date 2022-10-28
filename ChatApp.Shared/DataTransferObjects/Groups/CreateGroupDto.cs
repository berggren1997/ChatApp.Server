using ChatApp.Shared.DataTransferObjects.User;

namespace ChatApp.Shared.DataTransferObjects.Groups
{
    public class CreateGroupDto
    {
        public List<UserDto> Participants { get; set; } = new();
    }
}
