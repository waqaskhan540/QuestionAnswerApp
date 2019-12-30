using System;
using System.Collections.Generic;

namespace QnA.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        public AppUser User { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
