using MediatR;
using QnA.Application.Profile.Models;

namespace QnA.Application.Profile.Command
{
    public class UpdateProfileCommand : IRequest<ProfileDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int UserId { get; set; }
    }
}
