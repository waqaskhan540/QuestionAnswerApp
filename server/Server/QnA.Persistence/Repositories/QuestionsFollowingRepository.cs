using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Persistence.Repositories
{
    public class QuestionsFollowingRepository : IQuestionsFollowingRepository
    {
        private readonly IDatabaseContext _context;

        public QuestionsFollowingRepository(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<bool> QuestionsHasFollowers(int questionId)
        {
            return await _context.QuestionFollowings.AnyAsync(x => x.QuestionId == questionId);
        }
    }
}
