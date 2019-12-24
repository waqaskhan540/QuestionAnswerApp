using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.ApiModels
{
    public class UpdateProfileModel
    {
        [Required(ErrorMessage = "first name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "last name is required")]
        public string LastName { get; set; }

    }
}
