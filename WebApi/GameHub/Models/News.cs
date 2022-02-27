using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models
{
    // News model
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Title is required")]
        [MaxLength(150,ErrorMessage ="Maximum length is 150 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "About is required")]
        [MaxLength(500, ErrorMessage = "Maximum length is 500 characters")]
        public string About { get; set; }
        [Required(ErrorMessage = "Description 1 is required")]
        [Display(Name = "Description one")]
        public string DescriptionOne { get; set; }
        [Required(ErrorMessage = "Description 2 is required")]
        [Display(Name = "Description two")]
        public string DescriptionTwo { get; set; }
        [Required(ErrorMessage = "Description 3 is required")]
        [Display(Name = "Description three")]
        public string DescriptionThree { get; set; }
        [Display(Name ="Image one")]
        public string ImageOne { get; set; }

        [Display(Name = "Image Two")]
        public string ImageTwo { get; set; }
        [Display(Name = "Image Three")]
        public string ImageThree { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string Source { get; set; }
        public string VideoUrl { get; set; }
        public bool Trend { get; set; }
        public virtual Category Categories { get; set; }

        [NotMapped]
        public IFormFile ImageFileOne { get; set; }
        [NotMapped]
        public IFormFile ImageFileTwo { get; set; }
        [NotMapped]
        public IFormFile ImageFileThree { get; set; }

    }
}
