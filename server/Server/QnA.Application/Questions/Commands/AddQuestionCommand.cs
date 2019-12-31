using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Application.Questions.Commands
{
    public class AddQuestionCommand : IRequest<int>
    {
        public AddQuestionCommand(int userId,string questionText)
        {
            UserId = userId;
            QuestionText = questionText;
        }
        public int UserId { get; set; }
        public string QuestionText { get; set; }
    }
}
