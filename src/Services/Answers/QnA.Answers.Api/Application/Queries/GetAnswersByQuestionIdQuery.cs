using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using QnA.Answers.Api.Domain;

namespace QnA.Answers.Api.Queries
{
    public class GetAnswersByQuestionIdQuery : IRequest<IEnumerable<AnswerDto>>
    {
        
        public string QuestionId { get; set; }
    }
}
