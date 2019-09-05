using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.ApiModels
{
    public class QuestionViewModel
    {
        [Required]
        public string QuestionText { get; set; }

        public DateTime DateTime { get; set; }
    }
}
