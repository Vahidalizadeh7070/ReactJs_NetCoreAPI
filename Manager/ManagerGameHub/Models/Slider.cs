using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerGameHub.Models
{
    // Slider Model
    // This model is going to use in the view
    public class Slider
    {
        public int Id { get; set; }
        [Display(Name ="Image File")]
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage ="Caption is required")]
        [MaxLength(150, ErrorMessage = "The maximum lenght for Caption is 150 characters")]
        public string Caption { get; set; }
        [Required(ErrorMessage = "About is required")]
        [MaxLength(250, ErrorMessage = "The maximum lenght for About is 250 characters")]
        public string About { get; set; }
        [Required(ErrorMessage = "Link is required")]
        public string Link { get; set; }
    }
}
