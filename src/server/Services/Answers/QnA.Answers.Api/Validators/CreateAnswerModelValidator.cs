using FluentValidation;
using QnA.Answers.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Validators
{
    public class CreateAnswerModelValidator : AbstractValidator<CreateAnswerModel>
    {
        public CreateAnswerModelValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Answer body cannot be empty")
                .MaximumLength(500)
                    .WithMessage("Answer body cannot exceed 500 characters.");
        }
    }
}
