using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Models
{
    public class CreateDraftModel
    {
        public string Content { get; set; }
        public Guid QuestionId { get; set; }
    }
}
