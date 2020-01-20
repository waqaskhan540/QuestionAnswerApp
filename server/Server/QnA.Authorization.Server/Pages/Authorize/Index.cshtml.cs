using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Security;
using QnA.Authorization.Server.Models;
using QnA.Authorization.Server.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QnA.Authorization.Server.Pages.Authorize
{
    public class IndexModel : PageModel
    {
        private readonly IAuthorizationRequestValidator _validator;
        private readonly IDatabaseContext _context;
        private readonly IHashGenerator _hashGenerator;

        public IndexModel(
            IAuthorizationRequestValidator validator,
            IDatabaseContext context,
            IHashGenerator hashGenerator)
        {
            _validator = validator;
            _context = context;
            _hashGenerator = hashGenerator;
        }
        [BindProperty]
        public bool IsError { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "email address is required")]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public string SuccessMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public AuthorizationRequest AuthorizationRequest { get; set; }

        public IActionResult OnGetAsync()
        {
            //var authorizationRequest = Request.Query.AsAuthorizationRequest();
            //var validationResult = _validator.Validate(authorizationRequest);

            //if (validationResult.Error)
            //{
            //    IsError = true;
            //    ErrorMessage = validationResult.ValidationError;
            //    return Page();
            //}

            //AuthorizationRequest.ClientId = authorizationRequest.ClientId;
            //AuthorizationRequest.RedirectUri = authorizationRequest.RedirectUri;
            //AuthorizationRequest.ResponseType = authorizationRequest.ResponseType;
            //AuthorizationRequest.Scope = authorizationRequest.Scope;
            //AuthorizationRequest.State = authorizationRequest.State;



            return RedirectToPage("/Consent");

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return Page();
            }

            var isPasswordValid = _hashGenerator.CheckHash(user.PasswordHash, Password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,Email),
                new Claim(ClaimTypes.NameIdentifier,Email),
                new Claim("FullName",$"{user.FirstName} {user.LastName}")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties()
                );




            return new RedirectToPageResult("/Consent");
        }

    }
}
