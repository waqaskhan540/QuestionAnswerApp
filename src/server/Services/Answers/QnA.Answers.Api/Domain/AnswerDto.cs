using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Domain
{
    public class AnswerDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Actual content of the answer posted by user
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Reference to the question agains which the answer
        /// was posted
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Reference to the author of the answer
        /// </summary>
        public string AuthorId { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
