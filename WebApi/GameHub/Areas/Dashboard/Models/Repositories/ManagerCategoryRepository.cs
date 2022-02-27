using GameHub.Areas.Dashboard.Models.Interfaces;
using GameHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Areas.Dashboard.Models.Repositories
{
    // This repository inherits from ManagerCategoryInterface
    public class ManagerCategoryRepository : ManagerCategoryInterface
    {
        // DbContext field
        private readonly AppDbContext appDbContext;

        // Constructor
        public ManagerCategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // Add Category
        public async Task<Category> AddCategory(Category category)
        {
            var result = await appDbContext.Category.AddAsync(category);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Delete Category
        public async Task<Category> Delete(int id)
        {
            var result = await appDbContext.Category.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                appDbContext.Category.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        // Edit Category
        public async Task<Category> EditCategory(Category category)
        {
            var result = await appDbContext.Category.FirstOrDefaultAsync(e => e.Id == category.Id);
            if (result != null)
            {
                result.Id = category.Id;
                result.CategoryName= category.CategoryName;
                result.Date = DateTime.Now;
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        
        // Get all categories
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await appDbContext.Category.OrderByDescending(x => x.Id).ToListAsync(); 
        }

        // Get a specific category by id
        public async Task<Category> GetCategoryById(int id)
        {
            return await appDbContext.Category.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
