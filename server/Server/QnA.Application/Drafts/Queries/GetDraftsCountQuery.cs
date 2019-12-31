using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Application.Drafts.Queries
{
    public class GetDraftsCountQuery : IRequest<int>
    {
        public GetDraftsCountQuery(int userId)
        {
            UserId = userId;
        }
        public int UserId { get; set; }
    }
}
