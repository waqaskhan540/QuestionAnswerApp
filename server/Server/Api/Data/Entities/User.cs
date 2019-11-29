using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data.Entities
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
