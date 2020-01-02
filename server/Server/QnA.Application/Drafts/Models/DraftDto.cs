using QnA.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace QnA.Application.Drafts.Models
{
    public class DraftDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

        public static Expression<Func<Draft, DraftDto>> Projection
        {
            get
            {
                return d => new DraftDto
                {
                    Id = d.Id,
                    DateTime = d.DateTime,
                    Content = d.Content,
                    UserId = d.UserId,
                    QuestionId = d.QuestionId
                };
            }
        }
    }
}
