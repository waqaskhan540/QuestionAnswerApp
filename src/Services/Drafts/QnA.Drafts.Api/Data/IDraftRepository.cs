using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Data
{
    public interface IDraftRepository
    {
        Task<Draft> GetById(Guid draftId);
        Task<IEnumerable<Draft>> GetByAuthorId(string authorId);

        Task<Draft> Create(Draft draft);
        Task<Draft> Update(Draft draft);
        Task<Draft> Remove(Draft draft);
    }
}
