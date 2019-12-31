using MediatR;
using QnA.Application.Drafts.Models;
using QnA.Application.Interfaces;
using QnA.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QnA.Application.Drafts.Commands
{
    public class SaveDraftCommandHandler : IRequestHandler<SaveDraftCommand, SaveDraftViewModel>
    {
        private readonly IDatabaseContext _context;

        public SaveDraftCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<SaveDraftViewModel> Handle(SaveDraftCommand request, CancellationToken cancellationToken)
        {
            var draft = _context.Drafts.FirstOrDefault(x => x.UserId == request.UserId && x.QuestionId == request.QuestionId);

            if(draft != null)
            {
                draft.Content = request.Content;
                draft.DateTime = DateTime.Now;
                _context.Drafts.Update(draft);
            }else
            {
                var newDraft = new Draft
                {
                    UserId = request.UserId,
                    DateTime = DateTime.Now,
                    Content = request.Content,
                    QuestionId = request.QuestionId
                };

                await _context.Drafts.AddAsync(newDraft);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return new SaveDraftViewModel { Message = "Draft saved successfully." };
        }
    }
}
