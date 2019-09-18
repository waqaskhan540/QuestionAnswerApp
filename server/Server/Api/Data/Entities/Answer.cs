using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Entities
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerMarkup { get; set; }

        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int questionId { get; set; }
        public Question Question { get; set; }

    }
}
