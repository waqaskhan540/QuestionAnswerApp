using FluentValidation;
using QnA.Identity.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Identity.Api.Validators
{
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserModelValidator()
        {
            RuleFor(x => x.FirstName)
                    .NotEmpty()
                    .NotNull()
                        .WithMessage("First name cannot be empty")
                    .MaximumLength(100)
                        .WithMessage("First name cannot exceed 100 characters");

            RuleFor(x => x.LastName)
                   .NotEmpty()
                   .NotNull()
                       .WithMessage("Last name cannot be empty")
                   .MaximumLength(100)
                       .WithMessage("Last name cannot exceed 100 characters");

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
