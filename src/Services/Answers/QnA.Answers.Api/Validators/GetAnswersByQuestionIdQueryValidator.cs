using FluentValidation;
using QnA.Answers.Api.Queries;

namespace QnA.Answers.Api.Validators
{
    public class GetAnswersByQuestionIdQueryValidator : AbstractValidator<GetAnswersByQuestionIdQuery>
    {
        public GetAnswersByQuestionIdQueryValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotNull()
                .NotEmpty()
                    .WithMessage("Question Id can not be null.");
        }
    }
}
