using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Security;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Authorization.Server.Pages.Consent
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [BindProperties(SupportsGet = true)]
    public class IndexModel : PageModel
    {
        private readonly IDatabaseContext _context;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public IndexModel(IDatabaseContext context, IJwtTokenGenerator tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }

        public string ClientId { get; set; }
        public string AppName { get; set; }
        public string RedirectUri { get; set; }
        public string State { get; set; }
        public string Scope { get; set; }
        public string ResponseType { get; set; }

        public void OnGet()
        {
            ClientId = HttpContext.Session.GetString("client_id");
            ResponseType = HttpContext.Session.GetString("response_type");
            RedirectUri = HttpContext.Session.GetString("redirect_uri");
            State = HttpContext.Session.GetString("state");
            Scope = HttpContext.Session.GetString("scope");
            AppName = HttpContext.Session.GetString("app_name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = await _context.DeveloperApps.SingleOrDefaultAsync(x => x.AppId == Guid.Parse(ClientId));
            if (client == null)
            {
                ModelState.AddModelError("", "invalid client_id");
                return Page();
            }

            client.RequiresConsent = false;
            _context.DeveloperApps.Update(client);
            await _context.SaveChangesAsync(CancellationToken.None);

            var userId = client.UserId;
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);

            var token = _tokenGenerator.GenerateToken(user.LastName, user.Email, user.Id);
            return Redirect($"{RedirectUri}#{token}");

        }
    }
}
