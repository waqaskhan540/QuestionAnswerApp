using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionText { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }

        public AppUser User { get; set; }
    }
}
