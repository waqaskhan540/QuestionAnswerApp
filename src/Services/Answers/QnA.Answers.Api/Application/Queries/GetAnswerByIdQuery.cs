using MediatR;
using QnA.Answers.Api.Domain;

namespace QnA.Answers.Api.Queries
{
    public class GetAnswerByIdQuery : IRequest<AnswerDto>
    {
        public string AnswerId { get; set; }
    }
}
