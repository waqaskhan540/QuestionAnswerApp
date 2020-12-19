using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Questions.Api.Domain
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public DateTime PublishedDate { get; set; }
        
    }
}
