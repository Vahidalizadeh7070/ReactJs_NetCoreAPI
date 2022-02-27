using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models
{
    // FutureReleaseGame model
    public class FutureReleaseGame
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Platforms are required")]
        public string Platforms { get; set; }
        [Required(ErrorMessage = "Release date is required")]
        public DateTime ReleaseDate { get; set;  }
    }
}
