using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerGameHub.Models.ViewModel
{
    // Login view model 
    // We use this view model in the view to implement a model to pass all the properties that we need to the api
    // We do not need any RegisterViewModel since we are manager and we should login to
    // the management system by super user that we wrote the code in the api section
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
