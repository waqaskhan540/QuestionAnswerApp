using Api.ApiModels;
using Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QnA.Persistence;
using System.IO;
using System.Linq;
using System.Security.Claims;
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

        [HttpPost("api/profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody]UpdateProfileModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loggedUserId = HttpContext.GetLoggedUserId();
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == loggedUserId);

            if (null == user)
                return BadRequest();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return Ok(BaseResponse.Ok(new { user.FirstName, user.LastName }));
        }
    }
}
