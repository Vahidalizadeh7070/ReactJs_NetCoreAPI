using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models
{
    // Category model
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public System.Nullable<DateTime> Date { get; set; }
    }
}
