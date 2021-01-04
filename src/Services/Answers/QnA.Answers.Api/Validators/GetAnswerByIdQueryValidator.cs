using FluentValidation;
using QnA.Answers.Api.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Validators
{
    public class GetAnswerByIdQueryValidator : AbstractValidator<GetAnswerByIdQuery>
    {
        public GetAnswerByIdQueryValidator()
        {
            RuleFor(x => x.AnswerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Answer Id cannot be empty.");
        }
    }
}
