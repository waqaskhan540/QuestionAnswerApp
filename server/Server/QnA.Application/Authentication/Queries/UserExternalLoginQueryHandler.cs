using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Authentication.Models;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Security;
using QnA.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Authentication.Queries
{
    public class UserExternalLoginQueryHandler : IRequestHandler<UserExternalLoginQuery, UserLoginViewModel>
    {
        private readonly IExternalAuthenticationProvider _externalAuthProvider;
        private readonly IDatabaseContext _context;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public UserExternalLoginQueryHandler(
            IExternalAuthenticationProvider externalAuthProvider,
            IDatabaseContext context,
            IJwtTokenGenerator tokenGenerator
            )
        {
            _externalAuthProvider = externalAuthProvider;
            _context = context;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<UserLoginViewModel> Handle(UserExternalLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _externalAuthProvider.LoginExternal(request.Provider, request.AccessToken);
            if (user.Error != null)
                return new UserLoginViewModel
                {
                    Success = false,
                    Message = user.Error
                };

            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            string accesstoken = string.Empty;

            if (existingUser == null)
            {
                var newUser = AppUser.Create(
                    firstname: user.FirstName,
                    lastname: user.LastName,
                    email: user.Email,
                    passwordHash: ""
                    );
                newUser.ProfilePicture = user.Picture;

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync(CancellationToken.None);

                accesstoken = _tokenGenerator.GenerateToken(newUser.LastName, newUser.Email, newUser.Id);
                return new UserLoginViewModel
                {
                    Success = true,
                    AccessToken = accesstoken,
                    User = new UserInfo
                    {
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        Email = newUser.Email,
                        UserId = newUser.Id,
                        Image = newUser.ProfilePicture
                    }
                };
            }

            if (existingUser.ProfilePicture == null && user.Picture != null)
            {
                existingUser.ProfilePicture = user.Picture;
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync(CancellationToken.None);
            }

            accesstoken = _tokenGenerator.GenerateToken(existingUser.LastName, existingUser.Email, existingUser.Id);
            return new UserLoginViewModel
            {
                Success = true,
                AccessToken = accesstoken,
                User = new UserInfo
                {
                    FirstName = existingUser.FirstName,
                    LastName = existingUser.LastName,
                    Email = existingUser.Email,
                    UserId = existingUser.Id,
                    Image = existingUser.ProfilePicture
                }
            };

        }
    }
}
