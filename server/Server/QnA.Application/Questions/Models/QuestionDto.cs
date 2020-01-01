using QnA.Application.Answers.Models;
using QnA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QnA.Application.Questions.Models
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }

        public UserDto User { get; set; }
        public AnswerDto FeaturedAnswer { get; set; }
        public static Expression<Func<Question, QuestionDto>> Projection
        {
            get
            {
                return c => new QuestionDto
                {
                    Id = c.Id,
                    QuestionText = c.QuestionText,
                    DateTime = c.DateTime,
                    UserId = c.UserId,
                    User = new UserDto
                    {
                        FirstName = c.User.FirstName,
                        LastName = c.User.LastName,
                        Image = Convert.ToBase64String(c.User.ProfilePicture)
                    },
                    FeaturedAnswer = new AnswerDto
                    {

                    }

                };
            }
        }
    }

    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Image {get;set;}
    }
}
