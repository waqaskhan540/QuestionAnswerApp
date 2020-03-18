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
    public class AnswersRepository : IAnswersRepository
    {
        private readonly IDatabaseContext _context;

        public AnswersRepository(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Answer answer)
        {
            await _context.Answers.AddAsync(answer);
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestionId(int questionId)
        {
            return await _context.Answers
                                .Where(x => x.QuestionId == questionId)
                                .Include(q => q.User)
                                //.Select(AnswerDto.Projection)
                                .ToListAsync();
        }
    }
}
