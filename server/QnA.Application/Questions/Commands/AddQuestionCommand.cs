using MediatR;

namespace QnA.Application.Questions.Commands
{
    public class AddQuestionCommand : IRequest<int>
    {
        public AddQuestionCommand(int userId, string questionText)
        {
            UserId = userId;
            QuestionText = questionText;
        }
        public int UserId { get; set; }
        public string QuestionText { get; set; }
    }
}
