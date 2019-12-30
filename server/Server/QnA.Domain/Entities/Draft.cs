using System;

namespace QnA.Domain.Entities
{
    public class Draft
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

        public AppUser User { get; set; }
        public Question Question { get; set; }
    }
}
