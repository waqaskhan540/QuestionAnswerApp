using System.Collections.Generic;

namespace QnA.Domain.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public byte[] ProfilePicture { get; set; }

        public ICollection<Question> Questions { get; set; }
        public static AppUser Create(string firstname,string lastname,string email,string passwordHash)
        {
            return new AppUser
            {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                PasswordHash = passwordHash
            };
        }
    }
}
