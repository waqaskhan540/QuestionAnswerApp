using Microsoft.EntityFrameworkCore;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Persistence.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly IDatabaseContext _context;

        public QuestionsRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> QuestionExists(int questionId)
        {
            return await _context
                .Questions
                .AnyAsync(x => x.Id == questionId);
        }
    }

   
}
