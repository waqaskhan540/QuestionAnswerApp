using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseContext _context;

        public UserRepository(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task AddAsync(AppUser user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public void Update(AppUser user)
        {
            _context.Users.Update(user);
        }

        public async Task<bool> UserExists(string email)
        {
            return await _context.Users
                         .AnyAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        
    }
}
