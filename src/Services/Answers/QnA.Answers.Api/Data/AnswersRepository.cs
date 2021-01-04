using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Data
{
    public class AnswersRepository : IAnswersRepository
    {
        private readonly AnswersDbContext _dbContext;

        public AnswersRepository(AnswersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Answer answer)
        {
            await _dbContext.Answers.AddAsync(answer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Answer> GetById(string answerId)
        {
            return await _dbContext.Answers.FindAsync(answerId);
        }

        public async  Task<IEnumerable<Answer>> GetByQuestionId(string questionId)
        {
            return await _dbContext.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
        }
    }
}
