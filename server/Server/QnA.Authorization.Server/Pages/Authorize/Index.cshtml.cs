using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QnA.Authorization.Server
{
    public class IndexModel : PageModel
    {
        [EnableCors]
        public void OnPost()
        {
            var grantType = Request.Form["grant_type"];
            var code = Request.Form["grant_type"];
            var redirect_uri = Request.Form["redirect_uri"];
            var client_id = Request.Form["client_id"];
            var scopes = Request.Form["scope"];

            Redirect($"{redirect_uri}#access_token=dddkjfkdjeijkdfj&state=xyz&token_type=example&expires=3600");

        }
    }
}
