using MediatR;
using QnA.Application.Questions.Models;
using System.Collections.Generic;

namespace QnA.Application.Questions.Queries
{
    public class GetQuestionsQuery : IRequest<List<QuestionDto>>
    {
        public int UserId { get; set; }
    }
}
