using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Profile.Models;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Profile.Command
{
    public class UpdateProfilePictureCommandHandler : IRequestHandler<UpdateProfilePictureCommand, UpdateProfilePictureViewModel>
    {
        private readonly IDatabaseContext _context;

        public UpdateProfilePictureCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<UpdateProfilePictureViewModel> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user != null)
            {
                user.ProfilePicture = request.ProfilePicture;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);

                return new UpdateProfilePictureViewModel { Message = "Profile picture updated." };

            }
            return new UpdateProfilePictureViewModel { Message = "User not found." };
        }
    }
}
