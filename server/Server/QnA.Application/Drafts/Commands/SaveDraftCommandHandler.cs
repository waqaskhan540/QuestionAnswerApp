using MediatR;
using QnA.Application.Drafts.Models;
using QnA.Application.Exceptions;
using QnA.Application.Interfaces;
using QnA.Application.Interfaces.Repositories;
using QnA.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Drafts.Commands
{
    public class SaveDraftCommandHandler : IRequestHandler<SaveDraftCommand, SaveDraftViewModel>
    {
        
        private readonly IDraftRepository _draftRepository;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SaveDraftCommandHandler(
            IDraftRepository draftRepository,
            IQuestionsRepository questionsRepository,
            IUnitOfWork unitOfWork)
        {            
            _draftRepository = draftRepository;
            _questionsRepository = questionsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<SaveDraftViewModel> Handle(SaveDraftCommand request, CancellationToken cancellationToken)
        {
            if (!await _questionsRepository.QuestionExists(request.QuestionId))
                throw new InvalidQuestionException();

            var draft = await _draftRepository.GetByQuestionAndUser(request.UserId, request.QuestionId);

            if(draft != null)
            {
                draft.Content = request.Content;
                draft.DateTime = DateTime.Now;
                _draftRepository.Update(draft);
            }else
            {
                var newDraft = new Draft
                {
                    UserId = request.UserId,
                    DateTime = DateTime.Now,
                    Content = request.Content,
                    QuestionId = request.QuestionId
                };

                await _draftRepository.AddAsync(newDraft);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return new SaveDraftViewModel { Message = "Draft saved successfully." };
        }
    }
}
