using System.ComponentModel.DataAnnotations;

namespace QnA.Api.ApiModels
{
    public class AnswerViewModel
    {
        [Required(ErrorMessage = "Answer cannot be empty string")]
        public string Answer { get; set; }

        [Required(ErrorMessage = "Please provide a question Id.")]
        public int QuestionId { get; set; }
    }
}
