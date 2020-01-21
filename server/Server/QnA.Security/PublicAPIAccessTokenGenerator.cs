using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QnA.Application.Interfaces.Security;
using QnA.Security.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Security
{
    public class PublicAPIAccessTokenGenerator : IPublicApiAccessTokenGenerator<TokenResult>
    {
        private readonly SecurityOptions _securityOptions;
        public PublicAPIAccessTokenGenerator(IOptionsSnapshot<SecurityOptions> securityOptions)
        {
            _securityOptions = securityOptions.Value;
        }
        public Task<TokenResult> GenerateToken(string userId, string name, string email, IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_securityOptions.Secret);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString(), ClaimValueTypes.Integer32)
                }),
                IssuedAt = DateTime.UtcNow,
                Audience = _securityOptions.Audience,
                Issuer = _securityOptions.Issuer,
                Expires = DateTime.UtcNow.AddHours(_securityOptions.TokenExpiry),
                SigningCredentials = signingCredentials
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);
            var tokenResult = new TokenResult
            {
                AccessToken = token,
                Expiry = (tokenDescriptor.Expires.Value - DateTime.UtcNow).TotalSeconds,
                Type = "bearer"
            };

            return Task.FromResult(tokenResult);
        }
    }
}
