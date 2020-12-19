using Microsoft.EntityFrameworkCore;
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
    }
}
