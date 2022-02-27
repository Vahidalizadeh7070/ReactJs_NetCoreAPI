using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameHub.Models
{
    // User model
    public class User
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [MinLength(3,ErrorMessage ="The Email address isn't valid")]
        public string Email { get; set; }
        [JsonIgnore]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "The Password isn't valid")]
        public string Password { get; set; }

    }
}
