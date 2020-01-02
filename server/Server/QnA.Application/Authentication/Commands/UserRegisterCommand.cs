using MediatR;
using QnA.Application.Authentication.Models;

namespace QnA.Application.Authentication.Commands
{
    public class UserRegisterCommand : IRequest<UserLoginViewModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
