using QnA.Answers.Api.Models;
using QnA.Answers.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Domain
{
    public interface IAnswersService
    {
        Task<IEnumerable<AnswerDto>> GetByQuestionId(Guid questionId);
        Task<AnswerDto> GetById(Guid answerId);
        Task<AnswerResult> Create(CreateAnswerModel answerModel);
    }
}
