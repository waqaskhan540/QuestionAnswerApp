using FluentValidation;
using QnA.Identity.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Identity.Api.Validators
{
    public class LoginUserModelValidator : AbstractValidator<LoginUserModel>
    {
        public LoginUserModelValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
