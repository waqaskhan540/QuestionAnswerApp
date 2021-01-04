using FluentValidation;
using QnA.Questions.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Questions.Api.Validators
{
    public class CreateQuestionModelValidator : AbstractValidator<CreateQuestionModel>
    {
        public CreateQuestionModelValidator()
        {
            RuleFor(x => x.Title)
                    .NotEmpty()
                        .WithMessage("Question body cannot be empty")
                    .Length(10, 500)
                        .WithMessage("Question body must have at least 10 characters");
            
                    

        }
    }
}
