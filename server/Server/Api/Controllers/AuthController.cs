using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Api.ApiModels;
using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers
{
    [Route("api/[controller]")]

    public class AuthController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public AuthController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var existingUser = _dbContext.Users.FirstOrDefault(x => x.Email.Equals(model.Email, StringComparison.InvariantCultureIgnoreCase));
            if (existingUser != null)
                return BadRequest(BaseResponse.Error("user already exists."));

            var passwordHash = GetHash(model.Password);
            var user = AppUser.Create(
                firstname: model.FirstName,
                lastname: model.LastName,
                email: model.Email,
                passwordHash: passwordHash);

            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                var token = GenerateToken(user);
                return Ok(BaseResponse.Ok(new { access_token = token }));
            }
            catch (Exception ex)
            {
                return BadRequest(BaseResponse.Error("error processing request."));
            }


        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _dbContext.Users
                        .FirstOrDefault(x => x.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                return BadRequest(BaseResponse.Error("Invalid email or password"));

            var passwordHash = GetHash(model.Password);

            if (passwordHash != user.PasswordHash)
                return BadRequest(BaseResponse.Error("Invalid email or password"));

            try
            {
                var token = GenerateToken(user);
                return Ok(BaseResponse.Ok(new { access_token = token }));
            }
            catch (Exception ex)
            {
                return BadRequest(BaseResponse.Error("error processing request."));
            }


        }
        private string GetHash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }

        private string GenerateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("some_big_key_value_here_secret");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.LastName.ToString()),
                    new Claim(ClaimTypes.Email,user.Email.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}