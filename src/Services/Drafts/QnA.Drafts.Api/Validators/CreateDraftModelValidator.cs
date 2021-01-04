
using FluentValidation;
using QnA.Drafts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Validators
{
    public class CreateDraftModelValidator : AbstractValidator<CreateDraftModel>
    {
        public CreateDraftModelValidator()
        {
            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Draft body cannot be empty");

            RuleFor(x => x.QuestionId)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Question Id cannot be empty");
               
            
        }
    }
}
