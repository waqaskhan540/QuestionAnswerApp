using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Drafts.Api.Data
{
    public class Draft
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public Guid QuestionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
    }
}
