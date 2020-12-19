using QnA.Drafts.Api.Http;
using QnA.Drafts.Api.Models;
using QnA.Drafts.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Domain
{
    public interface IDraftService
    {
        Task<ICustomHttpResponse> Create(CreateDraftModel model);
        Task<ICustomHttpResponse<DraftDto>> GetById(Guid draftId);
        Task<ICustomHttpResponse<IEnumerable<DraftDto>>> GetByAuthorId(string authorId);
        Task<ICustomHttpResponse> Delete(Guid draftId);
        Task<ICustomHttpResponse> Update(UpdateDraftModel model);
    }
}
