using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Draft> Drafts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<Question>().HasKey(x => x.Id);
            modelBuilder.Entity<Question>()
                .Property(x => x.QuestionText)
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder.Entity<Question>()

                .HasOne(q => q.User)
                .WithMany(u => u.Questions)
                .HasForeignKey(q => q.UserId);

            modelBuilder.Entity<Answer>().HasKey(x => x.AnswerId);
            modelBuilder.Entity<Answer>()
                .Property(x => x.AnswerMarkup)
                .IsRequired();
            modelBuilder.Entity<Answer>()
                .Property(x => x.DateTime)
                .IsRequired();

            modelBuilder.Entity<Answer>()
                .HasOne(x => x.Question)
                .WithMany(x => x.Answers);




        }
    }
}
