using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Questions.Models;
using QnA.Application.Questions.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Questions.Handlers
{
    public class GetQuestionsQueryHandler : IRequestHandler<GetQuestionsQuery, List<QuestionDto>>
    {
        private readonly IDatabaseContext _context;

        public GetQuestionsQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<QuestionDto>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Questions
                .Where(x => x.UserId == request.UserId)
                .Select(QuestionDto.Projection)
                .ToListAsync();
            
        }
    }
}
