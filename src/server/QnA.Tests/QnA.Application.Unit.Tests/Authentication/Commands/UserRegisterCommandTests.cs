using Moq;
using QnA.Application.Authentication.Commands;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using QnA.Application.Interfaces.Security;
using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QnA.Application.Unit.Tests.Authentication.Commands
{
    public class UserRegisterCommandTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IHashGenerator> _hashGenerator;
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGenerator;
        private readonly Mock<IPlaceHolderImageProvider> _placeHolderImageProvider;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public UserRegisterCommandTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _hashGenerator = new Mock<IHashGenerator>();
            _jwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            _placeHolderImageProvider = new Mock<IPlaceHolderImageProvider>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }


        [Fact]
        public async void Register_User_Fails_When_User_Already_Exists()
        {
            
            _userRepository.Setup(u => u.UserExists("email@email.com"))
                .Returns(Task.FromResult(true));

            var command = new UserRegisterCommand
            {
                Email = "email@email.com",
                FirstName = "fname",
                LastName = "lname",
                Password = "password",
            };

            var commandHandler = new UserRegisterCommandHandler(
                _userRepository.Object
                , _hashGenerator.Object
                , _jwtTokenGenerator.Object
                , _placeHolderImageProvider.Object
                , _unitOfWork.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("User already exists.", result.Message);
        }

        [Fact]
        public async void User_Register_Succeeds_When_User_Does_Not_Already_Exists()
        {
            _userRepository.Setup(u => u.GetUserByEmail(It.IsAny<string>()))
               .Returns(Task.FromResult<AppUser>(null));

            _hashGenerator.Setup(h => h.ComputeHash(It.IsAny<string>()))
                .Returns("password-hash");

            _jwtTokenGenerator.Setup(t => t.GenerateToken(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns("jwt-token");

            var command = new UserRegisterCommand
            {
                Email = "email@email.com",
                FirstName = "fname",
                LastName = "lname",
                Password = "password",
            };

            var commandHandler = new UserRegisterCommandHandler(
                _userRepository.Object
                , _hashGenerator.Object
                , _jwtTokenGenerator.Object
                , _placeHolderImageProvider.Object
                , _unitOfWork.Object);

            var result = await commandHandler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            Assert.NotNull(result.AccessToken);
            Assert.NotNull(result.User);
           
        }
    }
}
