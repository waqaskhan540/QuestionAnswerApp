using System;

namespace QnA.Questions.Api.Data.Entities
{
    public class Question : BaseEntity
    {        
        public string Title { get; set; }       
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        public string AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
