using Microsoft.AspNetCore.Identity;
using QnA.Application.Interfaces.Authentication;
using QnA.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<(bool success, string errorMsg, string userId)> CreateUser(string firstname, string lastname, string email, string password)
        {
           
            var newUser = new ApplicationUser
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                 return (success: true, errorMsg: null, userId: newUser.Id);
            }

            var errorMsg = result.Errors.FirstOrDefault()?.Description;
            return (success: true, errorMsg: errorMsg, userId: null);
        }

        public async Task<IEnumerable<Claim>> GetUserClaims(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if(user != null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                return claims;
            }

            return new List<Claim>();


        }

        public async Task<(bool success,string userId)> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                var loginResult = await _signInManager.CheckPasswordSignInAsync(user, password,false);
                return (loginResult.Succeeded, user.Id);
            }
            return (false,null);
        }

    }
}
