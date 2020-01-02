using Microsoft.IdentityModel.Tokens;
using QnA.Application.Interfaces.Security;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QnA.Security
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(string name, string email, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("some_big_key_value_here_secret");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email,email),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString(),ClaimValueTypes.Integer32)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
