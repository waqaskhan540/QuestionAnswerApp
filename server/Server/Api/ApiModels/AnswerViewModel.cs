using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.ApiModels
{
    public class AnswerViewModel
    {
        [Required(ErrorMessage = "Answer cannot be empty string")]
        public string Answer { get; set; }

        [Required(ErrorMessage = "Please provide a question Id.")]
        public int QuestionId { get; set; }
    }
}
