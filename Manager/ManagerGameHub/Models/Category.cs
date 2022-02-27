using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerGameHub.Models
{
    // Category model
    // This model is going to use in the view 
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Category name is required")]
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }
        public DateTime Date { get; set; }
    }
}
