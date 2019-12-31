using MediatR;
using QnA.Application.Drafts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Application.Drafts.Queries
{
    public class GetDraftsQuery : IRequest<List<DraftDto>>
    {
        public GetDraftsQuery(int userId)
        {
            UserId = userId;
        }
        public int UserId { get; set; }
    }
}
