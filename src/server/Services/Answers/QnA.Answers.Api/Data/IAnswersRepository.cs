using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Data
{
    public interface IAnswersRepository
    {
        Task<IEnumerable<Answer>> GetByQuestionId(Guid questionId);
        Task<Answer> GetById(Guid answerId);
        Task Create(Answer answer);
    }
}
