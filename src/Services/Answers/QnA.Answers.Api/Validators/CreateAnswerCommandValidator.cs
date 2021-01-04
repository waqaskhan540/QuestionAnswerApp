using FluentValidation;
using QnA.Answers.Api.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Validators
{
    public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
    {
        public CreateAnswerCommandValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Answer body cannot be empty")
                .MaximumLength(500)
                    .WithMessage("Answer body cannot exceed 500 characters.");

            RuleFor(x => x.QuestionId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Question Id cannot be empty.");
        }
    }
}
