using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Domain.Entities;
using System.Reflection;

namespace QnA.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<SavedQuestion> SavedQuestions { get; set; }
        public DbSet<QuestionFollowing> QuestionFollowings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
