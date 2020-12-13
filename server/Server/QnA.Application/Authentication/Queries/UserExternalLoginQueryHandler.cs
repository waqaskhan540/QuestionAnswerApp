using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Authentication.Models;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using QnA.Application.Interfaces.Security;
using QnA.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Authentication.Queries
{
    public class UserExternalLoginQueryHandler : IRequestHandler<UserExternalLoginQuery, UserLoginViewModel>
    {
        private readonly IExternalAuthenticationProvider _externalAuthProvider;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public UserExternalLoginQueryHandler(
            IExternalAuthenticationProvider externalAuthProvider,
            IUserRepository userRepository,
            IJwtTokenGenerator tokenGenerator,
            IUnitOfWork unitOfWork
            )
        {
            _externalAuthProvider = externalAuthProvider;
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
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

            var existingUser = await _userRepository.GetUserByEmail(user.Email);
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

                await _userRepository.AddAsync(newUser);
                await _unitOfWork.SaveChangesAsync(CancellationToken.None);

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
                _userRepository.Update(existingUser);
                await _unitOfWork.SaveChangesAsync(CancellationToken.None);
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
