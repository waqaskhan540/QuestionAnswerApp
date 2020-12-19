using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QnA.Identity.Api.Data;
using QnA.Identity.Api.Results;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Identity.Api.Helpers
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<JwtTokenResult> GenerateAccessToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("could not find user info to generate access token.");

            string issuer = _configuration["Auth:Issuer"];
            string secret = _configuration["Auth:Secret"];
            string audience = _configuration["Auth:Audience"];

            int tokenExpiry = int.Parse(_configuration["Auth:TokenExpiry"]);

            var key = Encoding.UTF8.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(key);
            SigningCredentials signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                   new Claim(JwtClaimTypes.Subject, user.Id),
               }),
                Audience = audience,
                Claims = new Dictionary<string, object>
                {
                    {"first_name",user.FirstName },
                    {"last_name",user.LastName }
                },
                IssuedAt = DateTime.UtcNow,
                Issuer = issuer,
                Expires = DateTime.UtcNow.AddHours(tokenExpiry),
                SigningCredentials = signingCredentials
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtTokenResult
            {
                AccessToken = tokenHandler.WriteToken(token)
            };
        }
    }
}
