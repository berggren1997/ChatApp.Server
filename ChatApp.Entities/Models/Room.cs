namespace ChatApp.Entities.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomName { get; set; } = string.Empty;

        public List<Message> Messages { get; set; } = new();
    }
}
