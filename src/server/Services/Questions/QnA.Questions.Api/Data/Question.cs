using System;

namespace QnA.Questions.Api.Data
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
