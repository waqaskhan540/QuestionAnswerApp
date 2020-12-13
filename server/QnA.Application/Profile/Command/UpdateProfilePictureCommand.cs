using MediatR;
using QnA.Application.Profile.Models;

namespace QnA.Application.Profile.Command
{
    public class UpdateProfilePictureCommand : IRequest<UpdateProfilePictureViewModel>
    {
        public byte[] ProfilePicture { get; set; }
        public string FileType { get; set; }
        public int UserId { get; set; }
    }
}
