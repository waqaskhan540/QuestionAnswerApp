using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Answers.Models;
using QnA.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Answers.Queries
{
    public class GetAnswersByQuestionIdQueryHandler : IRequestHandler<GetAnswersByQuestionIdQuery, List<AnswerDto>>
    {
        private readonly IDatabaseContext _context;

        public GetAnswersByQuestionIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<AnswerDto>> Handle(GetAnswersByQuestionIdQuery request, CancellationToken cancellationToken)
        {
            var answers = await _context.Answers
                                .Where(x => x.QuestionId == request.QuestionId)
                                .Include(q => q.User)
                                .Select(AnswerDto.Projection)
                                .ToListAsync();
            return answers;
        }
    }
}
