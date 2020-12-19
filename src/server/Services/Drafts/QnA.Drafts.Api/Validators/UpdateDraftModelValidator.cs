using FluentValidation;
using QnA.Drafts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Validators
{
    public class UpdateDraftModelValidator : AbstractValidator<UpdateDraftModel>
    {
        public UpdateDraftModelValidator()
        {
            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty()
                .WithMessage("Answer body cannot be empty.");

            RuleFor(x => x.DraftId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Draft Id cannot be empty");
        }
    }
}
