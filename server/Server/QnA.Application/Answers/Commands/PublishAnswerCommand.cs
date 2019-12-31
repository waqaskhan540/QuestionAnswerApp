using MediatR;
using QnA.Application.Answers.Models;

namespace QnA.Application.Answers.Commands
{
    public class PublishAnswerCommand : IRequest<PublishAnswerViewModel>
    {
        public string Answer { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
    }
}
