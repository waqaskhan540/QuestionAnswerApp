using Api.ApiModels;
using Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QnA.Application.Profile.Command;
using System.IO;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class ProfileController : Controller
    {
        
        private readonly IMediator _mediator;
        public ProfileController( IMediator mediator)
        {            
            _mediator = mediator;
        }
        [HttpPost("api/profile/image")]
        [Authorize]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            var updateCommand = new UpdateProfilePictureCommand
            {
                UserId = HttpContext.GetLoggedUserId(),               
            };

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                updateCommand.ProfilePicture = stream.ToArray();                                
            }

            var response = await _mediator.Send(updateCommand);
            return Ok(BaseResponse.Ok(response.Message));
        }

        [HttpPost("api/profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody]UpdateProfileModel model)
        {
           
            var updateCommand = new UpdateProfileCommand
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserId = HttpContext.GetLoggedUserId()
            };

            var response = await _mediator.Send(updateCommand);
            return Ok(BaseResponse.Ok(response));
        }
    }
}
