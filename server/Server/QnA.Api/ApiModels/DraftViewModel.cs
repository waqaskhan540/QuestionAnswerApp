using System.ComponentModel.DataAnnotations;

namespace QnA.Api.ApiModels
{
    public class DraftViewModel
    {
        [Required(ErrorMessage = "Question Id is required")]
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Please specify some content")]
        public string Content { get; set; }
    }
}
