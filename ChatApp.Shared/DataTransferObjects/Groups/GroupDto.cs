using ChatApp.Shared.DataTransferObjects.GroupMessages;
using ChatApp.Shared.DataTransferObjects.User;

namespace ChatApp.Shared.DataTransferObjects.Groups
{
    public class GroupDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public List<UserDto> Participants { get; set; } = new();
        public List<GroupMessageDto> GroupMessages { get; set; } = new();
    }
}
