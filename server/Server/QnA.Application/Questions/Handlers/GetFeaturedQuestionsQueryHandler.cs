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
    public class GetFeaturedQuestionsQueryHandler : IRequestHandler<GetFeaturedQuestionsQuery, List<QuestionDto>>
    {
        private readonly IDatabaseContext _context;

        public GetFeaturedQuestionsQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<QuestionDto>> Handle(GetFeaturedQuestionsQuery request, CancellationToken cancellationToken)
        {
            var featured = await _context.Questions
                .Include(x => x.Answers)
                .Where(q => q.Answers.Count > 5)
                .OrderBy(q => q.DateTime)
                .Take(5)
                .Select(QuestionDto.Projection)
                .ToListAsync();

            if (featured.Any()) return featured;

            return await _context.Questions
                .Include(x => x.Answers)
                .OrderByDescending(q => q.Answers.Count)
                .Take(5)
                .Select(QuestionDto.Projection)
                .ToListAsync();

        }
    }
}
