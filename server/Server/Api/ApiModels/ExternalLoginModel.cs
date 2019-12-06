using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.ApiModels
{
    public class ExternalLoginModel
    {
        [Required(ErrorMessage = "A login provider is required.")]
        public string Provider { get; set; }
       
        [Required(ErrorMessage = "An access token is required.")]
        public string AccessToken { get; set; }
       
    }
}
