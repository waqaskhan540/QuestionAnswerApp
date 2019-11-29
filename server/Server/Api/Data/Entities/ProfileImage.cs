using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data.Entities
{
    public class ProfileImage
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }

        public int UserId { get; set; }

        public AppUser User { get; set; }
    }
}
