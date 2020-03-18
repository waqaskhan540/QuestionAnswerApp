using MediatR;
using Microsoft.EntityFrameworkCore;
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
        public GetAnswersByQuestionIdQueryHandler(IAnswersRepository answersRepository)
        {            
            _answersRepository = answersRepository;
        }
        public async Task<List<AnswerDto>> Handle(GetAnswersByQuestionIdQuery request, CancellationToken cancellationToken)
        {
            var answers = await _answersRepository
                            .GetAnswersByQuestionId(request.QuestionId);

            var list = new List<AnswerDto>();
            foreach (var ans in answers)
                list.Add(AnswerDto.FromEntity(ans));

            return list;
        }
    }
}
