using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Data
{
    public class DraftsContext : DbContext
    {
        public DraftsContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Draft> Drafts { get; set; }
    }
}
