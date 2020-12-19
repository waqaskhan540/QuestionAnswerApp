using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Drafts.Models;
using QnA.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Drafts.Queries
{
    public class GetDraftsQueryHandler : IRequestHandler<GetDraftsQuery, List<DraftDto>>
    {
        private readonly IDatabaseContext _context;

        public GetDraftsQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<DraftDto>> Handle(GetDraftsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Drafts
                                .Where(x => x.UserId == request.UserId)
                                .Select(DraftDto.Projection)
                                .ToListAsync();

        }
    }
}
