using MediatR;
using QnA.Application.Answers.Models;
using System.Collections.Generic;

namespace QnA.Application.Answers.Queries
{
    public class GetAnswersByQuestionIdQuery : IRequest<List<AnswerDto>>
    {
        public GetAnswersByQuestionIdQuery(int questionId)
        {
            QuestionId = questionId;
        }
        public int QuestionId { get; set; }
    }
}
