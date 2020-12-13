using MediatR;

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
