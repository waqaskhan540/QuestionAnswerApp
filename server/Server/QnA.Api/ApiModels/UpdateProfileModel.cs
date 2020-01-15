using System.ComponentModel.DataAnnotations;

namespace QnA.Api.ApiModels
{
    public class UpdateProfileModel
    {
        [Required(ErrorMessage = "first name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "last name is required")]
        public string LastName { get; set; }

    }
}
