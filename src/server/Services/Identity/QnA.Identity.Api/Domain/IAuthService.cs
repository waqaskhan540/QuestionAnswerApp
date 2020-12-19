using QnA.Identity.Api.Models;
using QnA.Identity.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Identity.Api.Domain
{
    public interface IAuthService
    {
        Task<AuthenticationResult> Login(LoginUserModel model);
        Task<AuthenticationResult> Register(RegisterUserModel model);
    }
}
