using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Application.Interfaces.Authentication
{
    public interface IUserService
    {        
        Task<(bool success,string errorMsg,string userId)> CreateUser(string firstname, string lastname, string email, string password);
        Task<(bool success, string userId)> Login(string email, string password);
        Task<IEnumerable<Claim>> GetUserClaims(string userId);
    }
}
