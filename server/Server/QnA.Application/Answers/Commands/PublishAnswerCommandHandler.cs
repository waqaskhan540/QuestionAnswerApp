using MediatR;
using Microsoft.EntityFrameworkCore;
using QnA.Application.Answers.Models;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using QnA.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Answers.Commands
{
    public class PublishAnswerCommandHandler : IRequestHandler<PublishAnswerCommand, PublishAnswerViewModel>
    {        
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IAnswersRepository _answersRepository;
        private readonly IQuestionsFollowingRepository _questionsFollowingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PublishAnswerCommandHandler(                        
            IQuestionsRepository questionsRepository,
            IAnswersRepository answersRepository,
            IQuestionsFollowingRepository questionsFollowingRepository,
            IUnitOfWork unitOfWork
            )
        {            
            _questionsRepository = questionsRepository;
            _answersRepository = answersRepository;
            _questionsFollowingRepository = questionsFollowingRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<PublishAnswerViewModel> Handle(PublishAnswerCommand request, CancellationToken cancellationToken)
        {
            var exists = await _questionsRepository.QuestionExists(request.QuestionId);
            if (!exists)
                return new PublishAnswerViewModel { Message = "Question does not exist." };

            var answer = new Answer
            {
                AnswerMarkup = request.Answer,
                DateTime = DateTime.UtcNow,
                QuestionId = request.QuestionId,
                UserId = request.UserId
            };

            
            await _answersRepository.AddAsync(answer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var hasFollowers = await _questionsFollowingRepository.QuestionsHasFollowers(request.QuestionId);
            return new PublishAnswerViewModel { 
                Message = "Answer published successfully",
                HasFollowers = hasFollowers
            };

        }
    }
}
