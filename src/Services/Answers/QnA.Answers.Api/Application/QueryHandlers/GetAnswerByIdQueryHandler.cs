using AutoMapper;
using FluentValidation;
using MediatR;
using QnA.Answers.Api.Data;
using QnA.Answers.Api.Domain;
using QnA.Answers.Api.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Answers.Api.QueryHandlers
{
    public class GetAnswerByIdQueryHandler : IRequestHandler<GetAnswerByIdQuery, AnswerDto>
    {
        private readonly IAnswersRepository _answersRepository;
        private readonly IValidator<GetAnswerByIdQuery> _validator;
        private readonly IMapper _mapper;

        public GetAnswerByIdQueryHandler(
            IAnswersRepository answersRepository,
            IValidator<GetAnswerByIdQuery> validator,
            IMapper mapper)
        {
            _answersRepository = answersRepository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<AnswerDto> Handle(GetAnswerByIdQuery request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var answer = await _answersRepository.GetById(request.AnswerId);
            return _mapper.Map<AnswerDto>(answer);
            
        }
    }
}
