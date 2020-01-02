using MediatR;
using QnA.Application.Authentication.Models;

namespace QnA.Application.Authentication.Queries
{
    public class UserExternalLoginQuery : IRequest<UserLoginViewModel>
    {
        public string Provider { get; set; }
        public string AccessToken { get; set; }
    }
}
