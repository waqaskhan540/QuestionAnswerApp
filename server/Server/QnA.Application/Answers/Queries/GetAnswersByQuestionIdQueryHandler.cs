using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Exceptions;
using QnA.Application.Answers.Models;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Answers.Queries
{
    public class GetAnswersByQuestionIdQueryHandler : IRequestHandler<GetAnswersByQuestionIdQuery, List<AnswerDto>>
    {       
        private readonly IAnswersRepository _answersRepository;
        private readonly IQuestionsRepository _questionsRepository;
        public GetAnswersByQuestionIdQueryHandler(
            IAnswersRepository answersRepository,
            IQuestionsRepository questionsRepository)
        {            
            _answersRepository = answersRepository;
            _questionsRepository = questionsRepository;
        }
        public async Task<List<AnswerDto>> Handle(GetAnswersByQuestionIdQuery request, CancellationToken cancellationToken)
        {
            bool questionExists = await _questionsRepository.QuestionExists(request.QuestionId);
            if (!questionExists) throw new InvalidQuestionException();

            var answers = await _answersRepository
                            .GetAnswersByQuestionId(request.QuestionId);

            var list = new List<AnswerDto>();
            foreach (var ans in answers)
                list.Add(AnswerDto.FromEntity(ans));

            return list;
        }
    }
}
