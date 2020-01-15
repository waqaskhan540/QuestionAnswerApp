using System.ComponentModel.DataAnnotations;

namespace QnA.Api.ApiModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
    }
}
