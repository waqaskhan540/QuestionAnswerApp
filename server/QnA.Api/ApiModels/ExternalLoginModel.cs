using System.ComponentModel.DataAnnotations;

namespace QnA.Api.ApiModels
{
    public class ExternalLoginModel
    {
        [Required(ErrorMessage = "A login provider is required.")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "An access token is required.")]
        public string AccessToken { get; set; }

    }
}
