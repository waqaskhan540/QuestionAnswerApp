using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Drafts.Queries
{
    public class GetDraftsCountQueryHandler : IRequestHandler<GetDraftsCountQuery, int>
    {
        private readonly IDatabaseContext _context;

        public GetDraftsCountQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(GetDraftsCountQuery request, CancellationToken cancellationToken)
        {
            var drafts = await _context.Drafts.Where(x => x.UserId == request.UserId).ToListAsync();
            return drafts.Count;
        }
    }
}
