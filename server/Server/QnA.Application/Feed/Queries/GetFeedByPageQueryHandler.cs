using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Questions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Feed.Queries
{
    public class GetFeedByPageQueryHandler : IRequestHandler<GetFeedByPageQuery, List<QuestionDto>>
    {
        private readonly IDatabaseContext _context;

        public GetFeedByPageQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<QuestionDto>> Handle(GetFeedByPageQuery request, CancellationToken cancellationToken)
        {
            var questions = await _context.Questions
                                 .OrderByDescending(x => x.DateTime)
                                 .Take(request.Page * 5)
                                 .Skip((request.Page - 1) * 5)
                                 .Select(QuestionDto.Projection)
                                 .ToListAsync();
            return questions;
        }
    }
}
