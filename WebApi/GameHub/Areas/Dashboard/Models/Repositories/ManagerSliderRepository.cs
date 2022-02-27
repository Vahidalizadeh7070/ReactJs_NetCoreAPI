using GameHub.Areas.Dashboard.Models.Interfaces;
using GameHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Areas.Dashboard.Models.Repositories
{
    // This repository inherits from ManagerSliderInterface
    public class ManagerSliderRepository : ManagerSliderInterface
    {
        // DbContext field
        private readonly AppDbContext appDbContext;

        // Constructor
        public ManagerSliderRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // Add Slider
        public async Task<Slider> AddSlider(Slider slider)
        {
            var result = await appDbContext.Slider.AddAsync(slider);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Delete Slider
        public async Task<Slider> Delete(int id)
        {
            var result = await appDbContext.Slider.FirstOrDefaultAsync(e => e.Id== id);
            if (result != null)
            {
                appDbContext.Slider.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        // Edit Slider
        public async Task<Slider> EditSlider(Slider slider)
        {
            if (slider != null)
            {
                var sliders = appDbContext.Slider.Attach(slider);
                sliders.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await appDbContext.SaveChangesAsync();
                return slider;

            }
            return null;
        }

        // Get all sliders
        public async Task<IEnumerable<Slider>> GetAll()
        {
            return await appDbContext.Slider.OrderByDescending(x => x.Id).ToListAsync();
        }

        // Get a specific slider by id
        public async Task<Slider> GetSliderById(int id)
        {
            return await appDbContext.Slider.OrderByDescending(x=>x.Id).FirstOrDefaultAsync(x => x.Id == id);
        }


    }
}
