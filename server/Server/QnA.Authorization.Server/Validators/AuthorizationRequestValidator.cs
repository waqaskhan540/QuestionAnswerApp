using QnA.Application.Interfaces;
using QnA.Authorization.Server.Models;
using System;
using System.Linq;

namespace QnA.Authorization.Server.Validators
{
    public class AuthorizationRequestValidator : IAuthorizationRequestValidator
    {
        private readonly IDatabaseContext _context;

        public AuthorizationRequestValidator(IDatabaseContext context)
        {
            _context = context;
        }
        public ValidationResult Validate(AuthorizationRequest input)
        {
            if (string.IsNullOrWhiteSpace(input.ClientId))
                return ValidationResult.Fail("invalid client_id");

            if (string.IsNullOrWhiteSpace(input.RedirectUri))
                return ValidationResult.Fail("invalid redirect_uri");

            if (string.IsNullOrWhiteSpace(input.ResponseType))
                return ValidationResult.Fail("invalid response_type");

            if (string.IsNullOrWhiteSpace(input.Scope))
                return ValidationResult.Fail("invalid scope");

            if (string.IsNullOrWhiteSpace(input.State))
                return ValidationResult.Fail("invalid state");

            if (input.ResponseType.Equals("code", System.StringComparison.OrdinalIgnoreCase))
                return ValidationResult.Fail("invalid response_type. response_type must be 'code'");

            if (!_context.DeveloperApps.Any(x => x.AppId == Guid.Parse(input.ClientId)))
                return ValidationResult.Fail("invalid client_id");

            if (!_context.RedirectUrls.Any(x => x.AppId == Guid.Parse(input.ClientId) && x.RedirectUri == input.RedirectUri))
                return ValidationResult.Fail("invalid redirect_uri");

            return ValidationResult.Success();


        }


    }

    public interface IAuthorizationRequestValidator : IValidator<AuthorizationRequest> { }
}
