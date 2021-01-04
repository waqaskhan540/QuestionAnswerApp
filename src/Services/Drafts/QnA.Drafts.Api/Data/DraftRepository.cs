using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Data
{
    public class DraftRepository : IDraftRepository
    {
        private readonly DraftsContext _draftsContext;

        public DraftRepository(DraftsContext draftsContext)
        {
            _draftsContext = draftsContext;
        }
        public async Task<Draft> Create(Draft draft)
        {
            await _draftsContext.Drafts.AddAsync(draft);
            await _draftsContext.SaveChangesAsync();
            return draft;
        }

        public async Task<IEnumerable<Draft>> GetByAuthorId(string authorId)
        {
            IEnumerable<Draft> drafts = await _draftsContext.Drafts
                            .Where(x => x.AuthorId == authorId).ToListAsync();

            return drafts;
        }

        public async Task<Draft> GetById(Guid draftId)
        {
            return await _draftsContext.Drafts.FindAsync(draftId);
        }

        public  async Task<Draft> Remove(Draft draft)
        {
             _draftsContext.Remove(draft);
            await _draftsContext.SaveChangesAsync();
            return draft;
        }

        public async Task<Draft> Update(Draft draft)
        {
            _draftsContext.Drafts.Update(draft);
            await _draftsContext.SaveChangesAsync();
            return draft;
        }
    }
}
