using AutoMapper;
using FluentValidation;
using MediatR;
using QnA.Answers.Api.Data;
using QnA.Answers.Api.Domain;
using QnA.Answers.Api.Queries;
using QnA.Answers.Api.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Answers.Api.QueryHandlers
{
    public class GetAnswerByQuestionIdQueryHandler : 
        IRequestHandler<GetAnswersByQuestionIdQuery, IEnumerable<AnswerDto>>
    {
        private readonly IAnswersRepository _answersRepository;
        private readonly IValidator<GetAnswersByQuestionIdQuery> _validator;
        private readonly IMapper _mapper;

        public GetAnswerByQuestionIdQueryHandler(
            IAnswersRepository answersRepository, 
            IValidator<GetAnswersByQuestionIdQuery> validator,
            IMapper mapper)
        {
            _answersRepository = answersRepository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AnswerDto>> Handle(GetAnswersByQuestionIdQuery request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var answers = await _answersRepository.GetByQuestionId(request.QuestionId);

            if (!answers.Any())
            {
                return Enumerable.Empty<AnswerDto>();
            }

            return _mapper.Map<IEnumerable<AnswerDto>>(answers);
        }
    }
}
