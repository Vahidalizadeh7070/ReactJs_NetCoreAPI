using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models
{
    // Slider model
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
        [MaxLength(150,ErrorMessage ="The maximum lenght for Caption is 150 characters")]
        public string Caption { get; set; }
        [MaxLength(250, ErrorMessage = "The maximum lenght for Caption is 250 characters")]
        public string About { get; set; }
        public string Link { get; set; }
    }
}
