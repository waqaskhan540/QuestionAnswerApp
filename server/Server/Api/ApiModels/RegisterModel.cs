using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ApiModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "first name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
    }
}
