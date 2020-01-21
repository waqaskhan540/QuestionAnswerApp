using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QnA.Application.Interfaces.Security
{
    public interface IPublicApiAccessTokenGenerator<TResult>
    {
        Task<TResult> GenerateToken(string userId, string name, string email, IEnumerable<Claim> claims);
    }
}
