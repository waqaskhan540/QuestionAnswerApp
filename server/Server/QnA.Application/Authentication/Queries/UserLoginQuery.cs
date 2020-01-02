using MediatR;
using QnA.Application.Authentication.Models;

namespace QnA.Application.Authentication.Queries
{
    public class UserLoginQuery : IRequest<UserLoginViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
