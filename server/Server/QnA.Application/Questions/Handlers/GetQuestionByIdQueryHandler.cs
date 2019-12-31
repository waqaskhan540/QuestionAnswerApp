using MediatR;
using QnA.Application.Interfaces;
using QnA.Application.Questions.Models;
using QnA.Application.Questions.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Questions.Handlers
{
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, QuestionDto>
    {
        private readonly IDatabaseContext _context;

        public GetQuestionByIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public Task<QuestionDto> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var question = _context.Questions
                .Where(x => x.Id == request.QuestionId)
                .Select(QuestionDto.Projection)
                .FirstOrDefault();

            return Task.FromResult(question);
        }

    }
}
