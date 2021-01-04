using AutoMapper;
using FluentValidation;
using MediatR;
using QnA.Answers.Api.Commands;
using QnA.Answers.Api.Data;
using QnA.Answers.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Answers.Api.CommandHandlers
{
    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, AnswerDto>
    {
        private readonly IValidator<CreateAnswerCommand> _validator;
        private readonly IAnswersRepository _answersRepository;
        private readonly IMapper _mapper;

        public CreateAnswerCommandHandler(
            IValidator<CreateAnswerCommand> validator,
            IAnswersRepository answersRepository,
            IMapper mapper)
        {
            _answersRepository = answersRepository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<AnswerDto> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var answer = new Answer
            {
                QuestionId = request.QuestionId,
                Description = request.Description
            };

            answer.AuthorId = Guid.NewGuid().ToString();
            await _answersRepository.Create(answer);

            return _mapper.Map<AnswerDto>(answer);


        }
    }
}
