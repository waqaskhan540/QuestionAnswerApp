using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnA.Questions.Api.Data
{
    public interface IQuestionsRepository
    {
        Task<IEnumerable<Question>> GetByAuthorId(string userId);
        Task<Question> GetById(Guid questionId);
        Task Create(Question question);
        Task Delete(Question question);
    }
}
