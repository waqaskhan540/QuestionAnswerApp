using MediatR;
using QnA.Application.Questions.Models;
using System.Collections.Generic;

namespace QnA.Application.Feed.Queries
{
    public class GetFeedByPageQuery : IRequest<List<QuestionDto>>
    {
        public GetFeedByPageQuery(int page)
        {
            Page = page;
        }
       public int Page { get; set; }
    }
}
