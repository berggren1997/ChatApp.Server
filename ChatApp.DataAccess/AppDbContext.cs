using ChatApp.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Conversation)
                .WithMany(x => x.Messages)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conversation>()
                .HasOne(x => x.CreatedByAppUser)
                .WithMany(x => x.Conversations)
                .OnDelete(DeleteBehavior.Restrict);
            
            //modelBuilder.Entity<Message>()
            //    .HasOne(a => a.Sender)
            //    .WithMany(x => x.ChatMessagesFromUser)
            //    .HasForeignKey(x => x.SenderId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Message>()
            //    .HasOne(a => a.Receiver)
            //    .WithMany(x => x.ChatMessagesToUser)
            //    .HasForeignKey(x => x.ReceiverId)
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
