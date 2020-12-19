using AutoMapper;
using FluentValidation;
using QnA.Answers.Api.Data;
using QnA.Answers.Api.Models;
using QnA.Answers.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Domain
{
    public class AnswersService : IAnswersService
    {
        private readonly IAnswersRepository _answersRepository;
        private readonly IValidator<CreateAnswerModel> _createAnswerModelValidator;
        private readonly IMapper _mapper;

        public AnswersService(
            IAnswersRepository answersRepository,
            IMapper mapper,
            IValidator<CreateAnswerModel> createAnswerModelValidator)
        {
            _answersRepository = answersRepository;
            _mapper = mapper;
            _createAnswerModelValidator = createAnswerModelValidator;
        }

        public async Task<AnswerResult> Create(CreateAnswerModel answerModel)
        {
            var validation = _createAnswerModelValidator.Validate(answerModel);
            if (!validation.IsValid)
            {
                string[] errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
                return new AnswerResult
                {
                    Succeeded = false,
                    Errors = errors
                };
            }

            Answer answerEntity = _mapper.Map<Answer>(answerModel);
            answerEntity.AuthorId = Guid.NewGuid().ToString();//TODO change after authentication

            await _answersRepository.Create(answerEntity);
            return new AnswerResult
            {
                Succeeded = true
            };

        }

        public async Task<AnswerDto> GetById(Guid answerId)
        {
            var answer = await _answersRepository.GetById(answerId);
            if (answer == null)
                return null;

            return _mapper.Map<AnswerDto>(answer);
        }

        public async Task<IEnumerable<AnswerDto>> GetByQuestionId(Guid questionId)
        {
            var answers = await _answersRepository.GetByQuestionId(questionId);
            if (!answers.Any())
                return Enumerable.Empty<AnswerDto>();

            return _mapper.Map<IEnumerable<AnswerDto>>(answers);
        }
    }
}
