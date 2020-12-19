using Microsoft.AspNetCore.Mvc;
using QnA.Identity.Api.Domain;
using QnA.Identity.Api.Models;
using System.Threading.Tasks;

namespace QnA.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// login user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginUserModel model)
        {
            var result = await _authService.Login(model);
            if (result.Succeeded)
            {
                return Ok(result.TokenResult);
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {
            var result = await _authService.Register(model);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

    }
}
