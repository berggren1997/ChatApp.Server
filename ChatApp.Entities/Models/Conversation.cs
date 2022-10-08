namespace ChatApp.Entities.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int CreatedByAppUserId { get; set; }
        public AppUser? CreatedByAppUser { get; set; }
        public List<Message> Messages { get; set; } = new();
    }
}
