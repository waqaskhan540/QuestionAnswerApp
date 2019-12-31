using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Questions.Commands;
using QnA.Application.Questions.Models;
using QnA.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Questions.Handlers
{
    public class SaveQuestionCommandHandler : IRequestHandler<SaveQuestionCommand, SaveQuestionViewModel>
    {
        private readonly IDatabaseContext _context;

        public SaveQuestionCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<SaveQuestionViewModel> Handle(SaveQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == request.QuestionId);
            if (null == question)
                return new SaveQuestionViewModel { Message = "Question does not exist." };

            var existing = await _context.SavedQuestions.AnyAsync(x => x.QuestionId == request.QuestionId && x.UserId == request.UserId);
            if (existing)
                return new SaveQuestionViewModel { Message = "Question already saved." };

            var savedQuestion = new SavedQuestion
            {
                UserId = request.UserId,
                QuestionId = request.UserId,
                DateTime = DateTime.Now
            };

            await _context.SavedQuestions.AddAsync(savedQuestion);
            await _context.SaveChangesAsync(cancellationToken);

            return new SaveQuestionViewModel { Message = "Question saved successfully." };

        }
    }
}
