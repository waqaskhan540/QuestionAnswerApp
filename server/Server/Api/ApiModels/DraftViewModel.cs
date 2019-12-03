using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.ApiModels
{
    public class DraftViewModel
    {
        [Required(ErrorMessage = "Question Id is required")]
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Please specify some content")]
        public string Content { get; set; }
    }
}
