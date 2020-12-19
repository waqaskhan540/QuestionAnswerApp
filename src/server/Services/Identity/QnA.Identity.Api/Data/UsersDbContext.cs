using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Identity.Api.Data
{
    public class UsersDbContext: IdentityDbContext<ApplicationUser>
    {
        public UsersDbContext(DbContextOptions options) : base(options)
        {
           
        }

        
    }
}
