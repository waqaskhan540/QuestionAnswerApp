using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Authentication.Models;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using QnA.Application.Interfaces.Security;
using QnA.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Authentication.Commands
{
    public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, UserLoginViewModel>
    {        
        private readonly IUserRepository _userRepository;
        private readonly IHashGenerator _hashGenerator;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IPlaceHolderImageProvider _placeholderImageProvider;
        private readonly IUnitOfWork _unitOfWork;

        public UserRegisterCommandHandler(            
            IUserRepository userRepository,
            IHashGenerator hashGenerator,
            IJwtTokenGenerator tokenGenerator,
            IPlaceHolderImageProvider placeholderImageProvider,
            IUnitOfWork unitOfWork
            )
        {            
            _userRepository = userRepository;
            _hashGenerator = hashGenerator;
            _tokenGenerator = tokenGenerator;
            _placeholderImageProvider = placeholderImageProvider;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserLoginViewModel> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userRepository.UserExists(request.Email);
            if (userExists)
                return new UserLoginViewModel
                {
                    Success = false,
                    Message = "User already exists."
                };

            var passwordHash = _hashGenerator.ComputeHash(request.Password);
            var user = AppUser.Create(
               firstname: request.FirstName,
               lastname: request.LastName,
               email: request.Email,
               passwordHash: passwordHash);

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(CancellationToken.None);

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
                    Image = user.ProfilePicture ?? _placeholderImageProvider.GetProfileImagePlaceHolder()
                }
            };

        }
    }
}
