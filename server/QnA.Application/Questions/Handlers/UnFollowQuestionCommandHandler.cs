using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Questions.Commands;
using QnA.Application.Questions.Models;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Questions.Handlers
{
    public class UnFollowQuestionCommandHandler : IRequestHandler<UnFollowQuestionCommand, FollowQuestionViewModel>
    {
        private readonly IDatabaseContext _context;

        public UnFollowQuestionCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<FollowQuestionViewModel> Handle(UnFollowQuestionCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context.Questions.AnyAsync(x => x.Id == request.QuestionId);
            if (!exists)
                return new FollowQuestionViewModel { Message = "Question does not exist." };

            var question = await _context.QuestionFollowings.SingleOrDefaultAsync(x => x.UserId == request.UserId && x.QuestionId == request.QuestionId);
            if (question == null)
                return new FollowQuestionViewModel { Message = "Invalid question Id." };

            _context.QuestionFollowings.Remove(question);
            await _context.SaveChangesAsync(cancellationToken);

            return new FollowQuestionViewModel { Message = "Question unfollowed.", QuestionId = request.QuestionId };


        }
    }
}
