using MediatR;
using QnA.Application.Questions.Models;

namespace QnA.Application.Questions.Commands
{
    public class UnFollowQuestionCommand : IRequest<FollowQuestionViewModel>
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
