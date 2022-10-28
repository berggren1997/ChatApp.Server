namespace ChatApp.Entities.Models
{
    public class Group
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppUser? CreatedBy { get; set; }
        public List<AppUser>? Participants { get; set; }
        public List<GroupMessage>? GroupMessages { get; set; }
    }
}
