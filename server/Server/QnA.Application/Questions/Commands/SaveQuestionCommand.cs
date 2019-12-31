using MediatR;
using QnA.Application.Questions.Models;

namespace QnA.Application.Questions.Commands
{
    public class SaveQuestionCommand : IRequest<SaveQuestionViewModel>
    {
        public SaveQuestionCommand(int questionId,int userId)
        {
            QuestionId = questionId;
            UserId = userId;
        }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
    }
}
