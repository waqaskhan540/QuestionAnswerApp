using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Application.Interfaces.Repositories
{
    public interface IAnswersRepository
    {
        Task AddAsync(Answer answer);
        Task<IEnumerable<Answer>> GetAnswersByQuestionId(int questionId);
    }
}
