using QnA.Identity.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Identity.Api.Helpers
{
    public interface IJwtTokenGenerator
    {
        Task<JwtTokenResult> GenerateAccessToken(string userId);
    }
}
