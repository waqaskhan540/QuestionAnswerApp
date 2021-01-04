using Microsoft.EntityFrameworkCore;
using QnA.Questions.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Questions.Api.Data
{
    public class QuestionsContext : DbContext
    {
        public QuestionsContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
               
        }

    }
}
