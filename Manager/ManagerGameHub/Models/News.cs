using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerGameHub.Models
{
    // News Model
    // This model is going to use in the view
    // There are some IFormFile that is going to use in upload section
    public class News
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(150, ErrorMessage = "Maximum length is 150 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "About is required")]
        [MaxLength(500, ErrorMessage = "Maximum length is 500 characters")]
        public string About { get; set; }
        [Required(ErrorMessage = "Description 1 is required")]
        [Display(Name = "Description one")]
        [DataType(DataType.MultilineText)]
        public string DescriptionOne { get; set; }
        [Display(Name = "Description two")]
        [DataType(DataType.MultilineText)]
        public string DescriptionTwo { get; set; }
        [Required(ErrorMessage = "Description 3 is required")]
        [Display(Name = "Description three")]
        [DataType(DataType.MultilineText)]
        public string DescriptionThree { get; set; }
        [Display(Name = "Image one")]
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
        [Display(Name = "Image File One")]
        public IFormFile ImageFileOne { get; set; }
        [NotMapped]
        [Display(Name = "Image File Two")]
        public IFormFile ImageFileTwo { get; set; }
        [NotMapped]
        [Display(Name = "Image File Three")]
        public IFormFile ImageFileThree { get; set; }
    }
}
