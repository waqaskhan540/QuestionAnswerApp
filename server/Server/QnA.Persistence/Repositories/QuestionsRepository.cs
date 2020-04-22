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
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly IDatabaseContext _context;

        public QuestionsRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Question>> GetQuestionsPagedData(int page)
        {
            var questions = await _context.Questions
                                 .OrderByDescending(x => x.DateTime)
                                 .Take(page * 5)
                                 .Skip((page - 1) * 5)
                                 //.Select(QuestionDto.Projection)
                                 .ToListAsync();
            return questions;
        }

        public async Task<bool> QuestionExists(int questionId)
        {
            return await _context
                .Questions
                .AnyAsync(x => x.Id == questionId);
        }
    }

   
}
