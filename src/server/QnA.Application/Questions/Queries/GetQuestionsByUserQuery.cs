using MediatR;
using QnA.Application.Questions.Models;
using System.Collections.Generic;

namespace QnA.Application.Questions.Queries
{
    public class GetQuestionsByUserQuery : IRequest<List<QuestionDto>>
    {
        public GetQuestionsByUserQuery(int userId)
        {
            UserId = userId;
        }
        public int UserId { get; set; }
    }
}
