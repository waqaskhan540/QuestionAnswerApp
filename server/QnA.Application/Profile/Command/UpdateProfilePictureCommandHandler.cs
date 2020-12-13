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
        private readonly IFileStorageProvider _fileStorageProvider;

        public UpdateProfilePictureCommandHandler(IDatabaseContext context, IFileStorageProvider fileStorageProvider)
        {
            _context = context;
            _fileStorageProvider = fileStorageProvider;
        }
        public async Task<UpdateProfilePictureViewModel> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user != null)
            {
                var imgFilePath = _fileStorageProvider.SaveFile(request.ProfilePicture, request.FileType);
                user.ProfilePicture = imgFilePath;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);

                return new UpdateProfilePictureViewModel { Message = "Profile picture updated." };

            }
            return new UpdateProfilePictureViewModel { Message = "User not found." };
        }
    }
}
