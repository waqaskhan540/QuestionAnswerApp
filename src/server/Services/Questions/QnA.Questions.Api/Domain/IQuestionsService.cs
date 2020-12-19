using QnA.Questions.Api.Models;
using QnA.Questions.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Questions.Api.Domain
{
    public interface IQuestionsService
    {
        Task<IEnumerable<QuestionDto>> GetByAuthorId(string authorId);
        Task<QuestionDto> GetById(Guid questionId);
        Task<QuestionResult> Create(CreateQuestionModel questionModel);
        Task<QuestionResult> Delete(Guid questionId);
    }
}
