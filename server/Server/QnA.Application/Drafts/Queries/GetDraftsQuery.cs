using MediatR;
using QnA.Application.Drafts.Models;
using System.Collections.Generic;

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
