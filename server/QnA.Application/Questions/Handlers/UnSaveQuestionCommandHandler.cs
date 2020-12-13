using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Questions.Commands;
using QnA.Application.Questions.Models;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Questions.Handlers
{
    public class UnSaveQuestionCommandHandler : IRequestHandler<UnSaveQuestionCommand, SaveQuestionViewModel>
    {
        private readonly IDatabaseContext _context;

        public UnSaveQuestionCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<SaveQuestionViewModel> Handle(UnSaveQuestionCommand request, CancellationToken cancellationToken)
        {
            var savedQuestion = await _context.SavedQuestions.SingleOrDefaultAsync(x => x.QuestionId == request.QuestionId && x.UserId == request.UserId);
            if (null == savedQuestion)
                return new SaveQuestionViewModel { Message = "No such question found." };

            _context.SavedQuestions.Remove(savedQuestion);
            await _context.SaveChangesAsync(cancellationToken);

            return new SaveQuestionViewModel { Message = "Question unsaved successfully.", QuestionId = request.QuestionId };
        }
    }
}
