using MediatR;
using QnA.Application.Questions.Models;

namespace QnA.Application.Questions.Commands
{
    public class UnSaveQuestionCommand : IRequest<SaveQuestionViewModel>
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
