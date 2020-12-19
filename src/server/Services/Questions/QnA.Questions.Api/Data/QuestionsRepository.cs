using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Questions.Api.Data
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly QuestionsContext _context;

        public QuestionsRepository(QuestionsContext context)
        {
            _context = context;
        }

        public async Task Create(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(Question question)
        {

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();


        }

        public async Task<Question> GetById(Guid questionId)
        {
            return
                 await _context.Questions.SingleOrDefaultAsync(x => x.Id == questionId);

        }

        public async Task<IEnumerable<Question>> GetByAuthorId(string userId)
        {
            return
                    await _context.Questions
                            .Where(x => x.AuthorId == userId)
                            .ToListAsync();

        }
    }
}
