using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Authentication.Models;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Security;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Authentication.Queries
{
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, UserLoginViewModel>
    {
        private readonly IDatabaseContext _context;
        private readonly IHashGenerator _hashGenerator;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IPlaceHolderImageProvider _placeHolderImageProvider;

        public UserLoginQueryHandler(
            IDatabaseContext context,
            IHashGenerator hashGenerator,
            IJwtTokenGenerator tokenGenerator,
            IPlaceHolderImageProvider placeHolderImageProvider
            )
        {
            _context = context;
            _hashGenerator = hashGenerator;
            _tokenGenerator = tokenGenerator;
            _placeHolderImageProvider = placeHolderImageProvider;
        }
        public async Task<UserLoginViewModel> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase));
            if (user == null)
                return new UserLoginViewModel
                {
                    Success = false,
                    Message = "Invalid email or password."
                };

            var passwordhash = _hashGenerator.ComputeHash(request.Password);
            if (!passwordhash.Equals(user.PasswordHash, StringComparison.OrdinalIgnoreCase))
                return new UserLoginViewModel
                {
                    Success = false,
                    Message = "Invalid email or password."
                };

            var accessToken = _tokenGenerator.GenerateToken(user.LastName, user.Email, user.Id);
            return new UserLoginViewModel
            {
                Success = true,
                AccessToken = accessToken,
                User = new UserInfo
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserId = user.Id,
                    Image = user.ProfilePicture != null ? user.ProfilePicture : _placeHolderImageProvider.GetProfileImagePlaceHolder()
                }
            };

        }
    }
}
