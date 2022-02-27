using GameHub.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Repositories
{
    // Slider Repo
    // This class is inherited from SliderInterface
    public class SliderRepository : SliderInterface
    {
        // Require field 
        private readonly AppDbContext db;

        // Constructor
        public SliderRepository(AppDbContext db)
        {
            this.db = db;
        }

        // List of slider
        public async Task<IEnumerable<Slider>> ListOfSlider()
        {
            return await db.Slider.OrderByDescending(x => x.Id).Take(2).ToListAsync();
        }
    }
}
