using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Security;
using QnA.Authorization.Server.Extensions;
using QnA.Authorization.Server.Validators;
using System.ComponentModel.DataAnnotations;

namespace QnA.Authorization.Server
{
    public class IndexModel : PageModel
    {
        private readonly IAuthorizationRequestValidator _validator;
        private readonly IDatabaseContext _context;
        private readonly IHashGenerator hashGenerator;

        public IndexModel(
            IAuthorizationRequestValidator validator,
            IDatabaseContext context,
            IHashGenerator hashGenerator)
        {
            _validator = validator;
            _context = context;
            this.hashGenerator = hashGenerator;
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

        public IActionResult OnGet()
        {
            var authorizationRequest = Request.Query.AsAuthorizationRequest();
            var validationResult = _validator.Validate(authorizationRequest);

            if (validationResult.Error)
            {
                IsError = true;
                ErrorMessage = validationResult.ValidationError;
                return Page();
            }

            return Page();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            SuccessMessage = "Login successfully";
            return Page();
        }

    }
}
