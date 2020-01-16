using Microsoft.AspNetCore.Mvc;

namespace QnA.Authorization.Server.Controllers
{
    public class AuthorizeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("")]
        public IActionResult Authorize()
        {
            var grantType = Request.Form["grant_type"];
            var code = Request.Form["grant_type"];
            var redirect_uri = Request.Form["redirect_uri"];
            var client_id = Request.Form["client_id"];
            var scopes = Request.Form["scope"];

            return Redirect($"{redirect_uri}#access_token=dddkjfkdjeijkdfj&state=xyz&token_type=example&expires=3600");
        }
    }
}
