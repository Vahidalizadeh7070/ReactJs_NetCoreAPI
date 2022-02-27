using GameHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Areas.Dashboard.Models.Interfaces
{
    // This interface is responsible for CRUD operation
    // The related repository will be inherited from it
    public interface ManagerCategoryInterface
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(Category category);
        Task<Category> EditCategory(Category category);
        Task<Category> Delete(int id);
    }
}
