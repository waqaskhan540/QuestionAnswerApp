using System;

namespace QnA.Domain.Entities
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerMarkup { get; set; }

        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int questionId { get; set; }
        public Question Question { get; set; }

    }
}
