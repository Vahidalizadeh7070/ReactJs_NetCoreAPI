using GameHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Areas.Dashboard.Models.Interfaces
{
    // This interface is responsible for CRUD operation
    // The related repository will be inherited from it
    public interface ManagerSliderInterface
    {
        Task<IEnumerable<Slider>> GetAll();
        Task<Slider> GetSliderById(int id);
        Task<Slider> AddSlider(Slider slider);
        Task<Slider> EditSlider(Slider slider);
        Task<Slider> Delete(int id);
    }
}
