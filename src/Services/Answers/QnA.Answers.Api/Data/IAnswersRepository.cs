using System.Collections.Generic;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Data
{
    public interface IAnswersRepository
    {
        Task<IEnumerable<Answer>> GetByQuestionId(string questionId);
        Task<Answer> GetById(string answerId);
        Task Create(Answer answer);
    }
}
