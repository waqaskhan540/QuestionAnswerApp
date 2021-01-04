using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Answers.Api.Data
{
    public class Answer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Actual content of the answer posted by user
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Reference to the question agains which the answer
        /// was posted
        /// </summary>
        public string QuestionId { get; set; }

        /// <summary>
        /// Reference to the author of the answer
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// published date of the answer
        /// </summary>
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// modified date of the answer
        /// </summary>
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    }
}
