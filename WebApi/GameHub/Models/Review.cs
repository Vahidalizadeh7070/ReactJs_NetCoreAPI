
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models
{
    // Review model
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "About is required")]
        [MinLength(5, ErrorMessage = "About length should be more than 20 characters")]
        [MaxLength(250, ErrorMessage = "About length should be less than 250 characters")]
        public string About { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public Category Category { get; set; }
        public string VideoUrl { get; set; }
        public ApplicationUser User { get; set; }

    }
}
