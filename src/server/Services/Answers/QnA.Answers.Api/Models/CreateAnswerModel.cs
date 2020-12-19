using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Models
{
    public class CreateAnswerModel
    {
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public Guid QuestionId { get; set; }
    }
}
