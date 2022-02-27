using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models
{
    // RateList model
    public class RateList
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [MaxLength(50,ErrorMessage ="The maximum lenght for name is 50")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Platforms are required")]
        [MaxLength(50, ErrorMessage = "The maximum lenght for name is 50")]
        public string Platforms { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int Rate { get; set; }
    }
}
