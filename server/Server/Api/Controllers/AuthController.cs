using Api.ApiModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QnA.Application.Authentication.Commands;
using QnA.Application.Authentication.Queries;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]

    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("external-login")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginModel model)
        {

            var query = new UserExternalLoginQuery
            {
                Provider = model.Provider,
                AccessToken = model.AccessToken
            };

            var response = await _mediator.Send(query);
            if (response.Success)
                return Ok(BaseResponse.Ok(response));

            return BadRequest(BaseResponse.Error(response.Message));


        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var command = new UserRegisterCommand
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password
            };

            var response = await _mediator.Send(command);
            if (response.Success)
                return Ok(BaseResponse.Ok(response));

            return BadRequest(BaseResponse.Error(response.Message));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var query = new UserLoginQuery
            {
                Email = model.Email,
                Password = model.Password
            };

            var response = await _mediator.Send(query);
            if (response.Success)
                return Ok(BaseResponse.Ok(response));

            return BadRequest(BaseResponse.Error(response.Message));
        }

    }
}
