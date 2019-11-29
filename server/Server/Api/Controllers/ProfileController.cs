using Api.Data;
using Api.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public ProfileController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("api/profile/image")]
        [Authorize]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            var loggedUserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == int.Parse(loggedUserId));
            using (var stream = new MemoryStream())
            {
                
                await file.CopyToAsync(stream);
                user.ProfilePicture = stream.ToArray();
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }

            
        }
    }
}
