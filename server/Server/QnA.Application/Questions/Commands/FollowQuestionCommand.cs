using MediatR;
using QnA.Application.Questions.Models;

namespace QnA.Application.Questions.Commands
{
    public class FollowQuestionCommand : IRequest<FollowQuestionViewModel>
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
    }
}
