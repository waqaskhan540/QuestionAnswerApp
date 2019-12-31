using MediatR;
using QnA.Application.Interfaces;
using QnA.Application.Questions.Commands;
using QnA.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Questions.Handlers
{
    public class AddQuestionCommandHandler : IRequestHandler<AddQuestionCommand, int>
    {
        private readonly IDatabaseContext _context;

        public AddQuestionCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Question
            {
                DateTime = DateTime.Now,
                UserId = request.UserId,
                QuestionText = request.QuestionText
            };

            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync(cancellationToken);

            return question.Id;

        }
    }
}
