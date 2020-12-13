using MediatR;
using QnA.Application.Questions.Models;

namespace QnA.Application.Questions.Queries
{
    public class GetQuestionByIdQuery : IRequest<QuestionDto>
    {
        public GetQuestionByIdQuery(int questionId)
        {
            QuestionId = questionId;
        }
        public int QuestionId { get; set; }
    }
}
