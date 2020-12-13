using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Questions.Commands;
using QnA.Application.Questions.Models;
using QnA.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Questions.Handlers
{
    public class FollowQuestionCommandHandler : IRequestHandler<FollowQuestionCommand, FollowQuestionViewModel>
    {
        private readonly IDatabaseContext _context;

        public FollowQuestionCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<FollowQuestionViewModel> Handle(FollowQuestionCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context.Questions.AnyAsync(x => x.Id == request.QuestionId);
            if (!exists)
                return new FollowQuestionViewModel { Message = "Question does not exist." };

            var isFollowing = await _context.QuestionFollowings.AnyAsync(x => x.QuestionId == request.QuestionId && x.UserId == request.UserId);
            if (isFollowing)
                return new FollowQuestionViewModel { Message = "Question already followed." };

            var following = new QuestionFollowing
            {
                UserId = request.UserId,
                QuestionId = request.QuestionId
            };

            await _context.QuestionFollowings.AddAsync(following);
            await _context.SaveChangesAsync(cancellationToken);

            return new FollowQuestionViewModel { Message = "Question followed successfully.", QuestionId = request.QuestionId };
        }
    }
}
