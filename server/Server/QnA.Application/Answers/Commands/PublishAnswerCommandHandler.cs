using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Answers.Models;
using QnA.Application.Interfaces;
using QnA.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Answers.Commands
{
    public class PublishAnswerCommandHandler : IRequestHandler<PublishAnswerCommand, PublishAnswerViewModel>
    {
        private readonly IDatabaseContext _context;

        public PublishAnswerCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<PublishAnswerViewModel> Handle(PublishAnswerCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context.Questions.AnyAsync(x => x.Id == request.QuestionId);
            if (!exists)
                return new PublishAnswerViewModel { Message = "Question does not exist." };

            var answer = new Answer
            {
                AnswerMarkup = request.Answer,
                DateTime = DateTime.Now,
                QuestionId = request.QuestionId,
                UserId = request.UserId
            };

            await _context.Answers.AddAsync(answer);
            await _context.SaveChangesAsync(cancellationToken);

            return new PublishAnswerViewModel { Message = "Answer published successfully" };

        }
    }
}
