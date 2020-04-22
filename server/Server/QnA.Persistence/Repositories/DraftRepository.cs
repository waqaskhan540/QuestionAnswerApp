using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Persistence.Repositories
{
    public class DraftRepository : IDraftRepository
    {
        private readonly IDatabaseContext _context;

        public DraftRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Draft draft)
        {
            await _context.Drafts.AddAsync(draft);
        }

       

        public async Task<Draft> GetByQuestionAndUser(int userId, int questionId)
        {
            return await _context
                    .Drafts
                    .SingleOrDefaultAsync(x => x.UserId == userId && x.QuestionId == questionId);
        }

        public void Update(Draft draft)
        {
            _context.Drafts.Update(draft);
        }
    }
}
