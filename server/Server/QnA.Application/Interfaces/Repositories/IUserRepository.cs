using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);
        Task AddAsync(AppUser user);
        Task<AppUser> GetUserByEmail(string email);
        void Update(AppUser user);
    }
}
