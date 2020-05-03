using QnA.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace QnA.Application.Questions.Models
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }

        public UserDto User { get; set; }

        public static QuestionDto FromEntity(Question entity)
        {

            if (entity.User == null)
                throw new ArgumentNullException(nameof(entity.User));

            return new QuestionDto
            {
                Id = entity.Id,
                QuestionText = entity.QuestionText,
                DateTime = entity.DateTime,
                UserId = entity.UserId,
                User = new UserDto
                {
                    FirstName = entity.User.FirstName,
                    LastName = entity.User.LastName,
                    //Image = Convert.ToBase64String(c.User.ProfilePicture)
                }
            };

        }
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
                        //Image = Convert.ToBase64String(c.User.ProfilePicture)
                    }
                };
            }
        }
    }

    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Image { get; set; }
    }
}
