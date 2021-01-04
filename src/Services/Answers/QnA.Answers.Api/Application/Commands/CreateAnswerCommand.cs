using MediatR;
using QnA.Answers.Api.Domain;
using System;

namespace QnA.Answers.Api.Commands
{
    public class CreateAnswerCommand : IRequest<AnswerDto>
    {
        public string Description { get; set; }        
        public string QuestionId { get; set; }
    }
}
