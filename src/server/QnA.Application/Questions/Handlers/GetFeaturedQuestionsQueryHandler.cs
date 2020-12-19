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
        private readonly IPlaceHolderImageProvider _placeHolderImageProvider;

        public GetFeaturedQuestionsQueryHandler(
            IDatabaseContext context,
            IPlaceHolderImageProvider placeHolderImageProvider)
        {
            _context = context;
            _placeHolderImageProvider = placeHolderImageProvider;
        }
        public async Task<List<QuestionDto>> Handle(GetFeaturedQuestionsQuery request, CancellationToken cancellationToken)
        {
            List<QuestionDto> questions = new List<QuestionDto>();

            var featured = await _context.Questions
                .Include(x => x.Answers)
                .Where(q => q.Answers.Count > 5)
                .OrderBy(q => q.DateTime)
                .Take(5)
                .Select(QuestionDto.Projection)
                .ToListAsync();

            if (featured.Any())
            {
                questions.AddRange(featured);
            }
            else
            {
                var mostAnswered = await _context.Questions
                .Include(x => x.Answers)
                .OrderByDescending(q => q.Answers.Count)
                .Take(5)
                .Select(QuestionDto.Projection)
                .ToListAsync();

                questions.AddRange(mostAnswered);
            }

            questions.ForEach(que =>
            {
                if (que.User.Image == null)
                    que.User.Image = _placeHolderImageProvider.GetProfileImagePlaceHolder();
            });


            return questions;
        }
    }
}
