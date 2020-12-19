using System;

namespace QnA.Drafts.Api.Models
{
    public class UpdateDraftModel
    {
        public string Content { get; set; }
        public Guid DraftId { get; set; }        
    }
}
