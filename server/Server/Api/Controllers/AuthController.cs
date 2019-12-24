using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json.Linq;

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


        [HttpPost("external-login")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            switch (model.Provider.ToLower())
            {
                case "facebook":
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync($"https://graph.facebook.com/v5.0/me?access_token={model.AccessToken}&fields=id,first_name,last_name,email,picture");
                        var result = JToken.Parse(await response.Content.ReadAsStringAsync());

                        var firstname = result["first_name"].ToString();
                        var lastname = result["last_name"].ToString();
                        var email = result["email"].ToString();
                        var picture = result["picture"]["data"]["url"].ToString();

                        var imageResponse = await httpClient.GetByteArrayAsync(picture);
                        var image = Convert.ToBase64String(imageResponse);

                        var existingUser = _dbContext.Users.FirstOrDefault(x => x.Email == email);
                        if (existingUser == null)
                        {
                            var user = AppUser.Create(
                                 firstname: firstname,
                                 lastname: lastname,
                                 email: email,
                                 passwordHash: "");

                            await _dbContext.Users.AddAsync(user);
                            await _dbContext.SaveChangesAsync();

                            var accessToken = GenerateToken(lastname, email, user.Id);
                            var tokenResponse = new
                            {
                                access_token = accessToken,
                                user = new
                                {
                                    firstname = user.FirstName,
                                    lastname = user.LastName,
                                    email = user.Email,
                                    userId = user.Id,
                                    image = image
                                }
                            };
                            return Ok(BaseResponse.Ok(tokenResponse));
                        }

                        var token = GenerateToken(existingUser.LastName, existingUser.Email, existingUser.Id);
                        var resp = new
                        {
                            access_token = token,
                            user = new
                            {
                                firstname = existingUser.FirstName,
                                lastname = existingUser.LastName,
                                email = existingUser.Email,
                                userId = existingUser.Id,
                                image = image
                            }
                        };

                        return Ok(BaseResponse.Ok(resp));
                    }
                case "google":
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync($"https://www.googleapis.com/oauth2/v2/userinfo?access_token={model.AccessToken}");
                        var result = JToken.Parse(await response.Content.ReadAsStringAsync());

                        var firstname = result["given_name"].ToString();
                        var lastname = result["family_name"].ToString();
                        var email = result["email"].ToString();
                        var picture = result["picture"].ToString();

                        var imageResponse = await httpClient.GetByteArrayAsync(picture);
                        //var image = Convert.ToBase64String(imageResponse);

                        var existingUser = _dbContext.Users.FirstOrDefault(x => x.Email == email);
                        if (existingUser == null)
                        {
                            var user = AppUser.Create(
                                 firstname: firstname,
                                 lastname: lastname,
                                 email: email,                                 
                                 passwordHash: "");

                            user.ProfilePicture = imageResponse;
                            await _dbContext.Users.AddAsync(user);
                            await _dbContext.SaveChangesAsync();

                            var accessToken = GenerateToken(lastname, email, user.Id);
                            var tokenResponse = new
                            {
                                access_token = accessToken,
                                user = new
                                {
                                    firstname = user.FirstName,
                                    lastname = user.LastName,
                                    email = user.Email,
                                    userId = user.Id,
                                    image = Convert.ToBase64String(imageResponse)
                                }
                            };
                            return Ok(BaseResponse.Ok(tokenResponse));
                        }

                        var token = GenerateToken(existingUser.LastName, existingUser.Email, existingUser.Id);
                        var resp = new
                        {
                            access_token = token,
                            user = new
                            {
                                firstname = existingUser.FirstName,
                                lastname = existingUser.LastName,
                                email = existingUser.Email,
                                userId = existingUser.Id,
                                image = Convert.ToBase64String(existingUser.ProfilePicture)
                    }
                        };

                        return Ok(BaseResponse.Ok(resp));
                    }
            }

            return Ok();
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
                var token = GenerateToken(user.LastName, user.Email, user.Id);
                var tokenResponse = new
                {
                    access_token = token,
                    user = new
                    {
                        firstname = user.FirstName,
                        lastname = user.LastName,
                        email = user.Email,
                        userId = user.Id,
                        image = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null
                    }
                };
                return Ok(BaseResponse.Ok(tokenResponse));
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

                var accessToken = GenerateToken(user.LastName, user.Email, user.Id);
                var tokenResponse = new
                {
                    access_token = accessToken,
                    user = new
                    {
                        firstname = user.FirstName,
                        lastname = user.LastName,
                        email = user.Email,
                        userId = user.Id,
                        image = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null
                    }
                };
                return Ok(BaseResponse.Ok(tokenResponse));
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

        private string GenerateToken(string name, string email, int userId)
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