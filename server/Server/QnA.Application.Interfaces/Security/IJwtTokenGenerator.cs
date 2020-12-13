using System.Collections.Generic;
using System.Security.Claims;

namespace QnA.Application.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, IEnumerable<Claim> claims);
    }
}
