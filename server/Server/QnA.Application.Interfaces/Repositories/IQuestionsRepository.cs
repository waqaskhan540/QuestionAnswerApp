using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Application.Interfaces.Repositories
{
    public interface IQuestionsRepository
    {
        Task<bool> QuestionExists(int questionId);
        Task<IEnumerable<Question>> GetQuestionsPagedData(int page);
    }
}
