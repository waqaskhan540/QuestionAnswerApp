using MediatR;
using QnA.Application.Drafts.Models;

namespace QnA.Application.Drafts.Commands
{
    public class SaveDraftCommand : IRequest<SaveDraftViewModel>
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public int QuestionId { get; set; }
    }
}
