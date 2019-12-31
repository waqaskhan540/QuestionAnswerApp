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
    public class GetQuestionsByUserQueryHandler : IRequestHandler<GetQuestionsByUserQuery, List<QuestionDto>>
    {
        private readonly IDatabaseContext _context;

        public GetQuestionsByUserQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<QuestionDto>> Handle(GetQuestionsByUserQuery request, CancellationToken cancellationToken)
        {
            return await _context.Questions
                                .Where(q => q.UserId == request.UserId)
                                .Select(QuestionDto.Projection)
                                .OrderByDescending(x => x.DateTime)
                                .ToListAsync();


        }
    }
}
