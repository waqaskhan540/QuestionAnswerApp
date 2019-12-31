using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Application.Answers.Models
{
    public class AnswerDto
    {
        public int AnswerId { get; set; }
        public string AnswerMarkup { get; set; }
        public DateTime DateTime { get; set; }

        public UserDto User { get; set; }
        public static Expression<Func<Answer, AnswerDto>> Projection
        {
            get
            {
                return c => new AnswerDto
                {
                    AnswerId = c.AnswerId,
                    AnswerMarkup = c.AnswerMarkup,
                    DateTime = c.DateTime,
                    User = new UserDto
                    {
                        FirstName = c.User.FirstName,
                        LastName = c.User.LastName,
                        Email = c.User.Email
                    }
                };
            }
        }

    }

    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
