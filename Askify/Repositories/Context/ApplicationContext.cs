using Askify.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Askify.Repositories.Context
{
    public class ApplicationContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public ApplicationContext(DbContextOptions options):base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EndUser>(e =>
            {
                e.ToTable("EndUsers");
                e.HasKey(e => e.Id);

                e.HasMany(u => u.Followers)
                .WithMany(u => u.Following)
                .UsingEntity(j => j.ToTable("UsersFollowers"));

                e.HasOne(x => x.IdentityUser)
                .WithOne(x => x.EndUser)
                .HasForeignKey<AppUser>(x => x.EndUserId);

            });

            builder.Entity<Question>(q =>
            {
                q.ToTable("Questions");
                q.HasKey(e => e.Id);

                q.Property(e => e.Text).HasMaxLength(3000);

                q.HasOne(e => e.Sender)
                .WithMany(e => e.SentQuestions)
                .HasForeignKey(e => e.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

                q.HasOne(e => e.Receiver)
                .WithMany(e => e.ReceivedQuestions)
                .HasForeignKey(c => c.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

                q.HasOne(x => x.ParentQuestion)
                .WithMany(x => x.ChildrenQuestions)
                .HasForeignKey(x => x.ParentQuestionId);

            });
            
            builder.Entity<Answer>(a =>
            {
                a.HasKey(a => a.Id);

                a.HasOne(q => q.Question)
                .WithMany(ans => ans.Answers)
                .HasForeignKey(ans => ans.QuestionId);

                a.HasOne(s => s.Sender)
                .WithMany(s => s.SentAnswers)
                .HasForeignKey(s => s.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

                a.HasOne(r => r.Receiver)
                .WithMany(r => r.ReceivedAnswers)
                .HasForeignKey(r => r.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<UserAnswerLikes>(u =>
            {
                u.HasKey(us => new { us.UserId, us.AnswerID});

                u.HasOne(x => x.Answer)
                .WithMany(x => x.UsersLikes)
                .HasForeignKey(x => x.AnswerID);

                u.HasOne(x => x.User)
                .WithMany(x => x.LikedAnswers)
                .HasForeignKey(x => x.UserId);
            });

            builder.Entity<Notification>(x =>
            {
                x.HasKey(x => x.Id);

                x.HasOne(x => x.Answer)
                .WithMany(x => x.Notification)
                .HasForeignKey(x => x.AnswerId)
                .OnDelete(DeleteBehavior.Cascade);

                x.HasOne(x => x.Receiver)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            base.OnModelCreating(builder);
        }
        public DbSet<EndUser> EndUsers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserAnswerLikes> UserAnswerLikes { get; set; }
    }
}
