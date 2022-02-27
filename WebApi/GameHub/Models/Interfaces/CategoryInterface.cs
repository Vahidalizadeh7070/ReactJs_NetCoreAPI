using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Interfaces
{
    // Category Interface
    public interface CategoryInterface
    {
        Task<IEnumerable<Category>> ListOfCategory();
        Task<Category> Category(int id);
    }
}
