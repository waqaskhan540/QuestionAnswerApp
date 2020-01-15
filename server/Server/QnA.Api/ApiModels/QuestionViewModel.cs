using System.ComponentModel.DataAnnotations;

namespace QnA.Api.ApiModels
{
    public class QuestionViewModel
    {
        [Required]
        public string QuestionText { get; set; }


    }
}
