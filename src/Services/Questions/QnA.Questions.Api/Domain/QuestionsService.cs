using AutoMapper;
using FluentValidation;
using QnA.Questions.Api.Data;
using QnA.Questions.Api.Data.Entities;
using QnA.Questions.Api.Helpers;
using QnA.Questions.Api.Models;
using QnA.Questions.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QnA.Questions.Api.Domain
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateQuestionModel> _createQuestionModelValidator;
        private readonly ICurrentUser _currentUser;

        public QuestionsService(
            IQuestionsRepository questionsRepository,
            IMapper mapper,
            IValidator<CreateQuestionModel> createQuestionModelValidator,
            ICurrentUser currentUser)
        {
            _questionsRepository = questionsRepository;
            _mapper = mapper;
            _createQuestionModelValidator = createQuestionModelValidator;
            _currentUser = currentUser;
        }

        public async Task<QuestionResult> Create(CreateQuestionModel questionModel)
        {
            var validation = _createQuestionModelValidator.Validate(questionModel);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(x => x.ErrorMessage).ToArray();
                var response = new QuestionResult
                {
                    Succeeded = false,
                    Errors = errors
                };
                return response;
            }

            var questionEntity = _mapper.Map<Question>(questionModel);
            questionEntity.AuthorId = _currentUser.GetUserId();

            await _questionsRepository.Create(questionEntity);

            return new QuestionResult
            {
                Succeeded = true
            };
        }

        public async Task<QuestionResult> Delete(Guid questionId)
        {
            var question = await _questionsRepository.GetById(questionId);
            if(question == null)
            {
                return new QuestionResult
                {
                    Succeeded = false,
                    Errors = new string[] { "Question not found." }
                };
            }

            await _questionsRepository.Delete(question);
            return new QuestionResult
            {
                Succeeded = true
            };
        }

        public async Task<QuestionDto> GetById(Guid questionId)
        {
            var question = await _questionsRepository.GetById(questionId);
            return _mapper.Map<QuestionDto>(question);
        }

        public async Task<IEnumerable<QuestionDto>> GetByAuthorId(string authorId)
        {
            var questions = await _questionsRepository.GetByAuthorId(authorId);
            return _mapper.Map<IEnumerable<QuestionDto>>(questions);                 
        }
    }
}
