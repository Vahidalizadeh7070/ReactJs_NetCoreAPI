using GameHub.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Repositories
{
    // Category Repo
    // This class is inherited from CategoryInterface
    public class CategoryRepository : CategoryInterface
    {
        // Require field 
        private readonly AppDbContext db;

        // Constructor
        public CategoryRepository(AppDbContext db)
        {
            this.db = db;
        }

        // This method is going to retrieve a category
        public async Task<Category> Category(int id)
        {
            return await db.Category.FirstOrDefaultAsync(e => e.Id == id);
        }

        // This method is going to retrieve list of category
        public async Task<IEnumerable<Category>> ListOfCategory()
        {
            return await db.Category.ToListAsync();
        }
    }
}
