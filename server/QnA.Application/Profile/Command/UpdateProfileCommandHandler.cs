using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Profile.Models;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Profile.Command
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ProfileDto>
    {
        private readonly IDatabaseContext _context;

        public UpdateProfileCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<ProfileDto> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
                return new ProfileDto(); //TODO: change this later

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new ProfileDto { FirstName = user.FirstName, LastName = user.LastName };


        }
    }
}
