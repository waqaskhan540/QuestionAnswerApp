using FluentValidation;
using Microsoft.AspNetCore.Identity;
using QnA.Identity.Api.Data;
using QnA.Identity.Api.Helpers;
using QnA.Identity.Api.Models;
using QnA.Identity.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Identity.Api.Domain
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IValidator<RegisterUserModel> _registerUserModelValidator;
        private readonly IValidator<LoginUserModel> _loginUserValidator;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IValidator<RegisterUserModel> registerUserModelValidator,
            IValidator<LoginUserModel> loginUserModelValidator,
            IJwtTokenGenerator tokenGenerator
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _registerUserModelValidator = registerUserModelValidator;
            _loginUserValidator = loginUserModelValidator;
            _jwtTokenGenerator = tokenGenerator;
        }
        public async Task<AuthenticationResult> Login(LoginUserModel model)
        {

            var validation = _loginUserValidator.Validate(model);
            if(!validation.IsValid)
            {
                string[] errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
                return new AuthenticationResult
                {
                    Succeeded = false,
                    Errors = errors
                };
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return new AuthenticationResult
                {
                    Succeeded = false,
                    Errors = new string[] { "Invalid email or password." }
                };
            }

            var result = await _signInManager
                               .PasswordSignInAsync(user, model.Password, isPersistent: false,lockoutOnFailure:true);
            if (result.Succeeded)
            {
                JwtTokenResult tokenResult = await _jwtTokenGenerator.GenerateAccessToken(user.Id);
                return new AuthenticationResult
                {
                    Succeeded = true,
                    TokenResult = tokenResult
                };
            }

            return new AuthenticationResult
            {
                Succeeded = false,
                Errors = new string[] { "Invalid email or password." }
            };
        }

        public async Task<AuthenticationResult> Register(RegisterUserModel model)
        {
            var validation = _registerUserModelValidator.Validate(model);
            if (!validation.IsValid)
            {
                string[] errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
                return new AuthenticationResult
                {
                    Succeeded = false,
                    Errors = errors
                };
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user != null)
            {
                return new AuthenticationResult
                {
                    Succeeded = false,
                    Errors = new string[] { "user with this email already exists" }
                };
            }

            var newUser = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,                
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                JwtTokenResult tokenResult = await _jwtTokenGenerator.GenerateAccessToken(newUser.Id);
                return new AuthenticationResult
                {
                    Succeeded = true,
                    TokenResult = tokenResult
                };
            }

            return new AuthenticationResult
            {
                Succeeded = false,
                Errors = result.Errors.Select(x => x.Description).ToArray()
            };
        }
    }
}
