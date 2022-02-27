using GameHub.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Repositories
{
    // News Repo
    // This class is inherited from NewsInterface
    public class NewsRepository : NewsInterface
    {
        // Require field 
        private readonly AppDbContext db;

        // Constructor
        public NewsRepository(AppDbContext db)
        {
            this.db = db;
        }

        // Get news by categoryId
        public async Task<IList<News>> GetNewsByCategoryId(int cateporyId)
        {
            return await db.News.OrderByDescending(x => x.Id).Include(x => x.Categories).Where(e => e.CategoryId == cateporyId).Take(3).ToListAsync();
        }

        // Get news by id
        public async Task<News> GetNewsById(int id)
        {
            return await db.News.Include(x=>x.Categories).FirstOrDefaultAsync(e => e.Id == id);
        }

        // Get news by trend
        public async Task<IList<News>> GetNewsByTrend()
        {
            return await db.News.Include(x => x.Categories).OrderByDescending(x=>x.Id).Where(x=>x.Trend==true).Take(4).ToListAsync();
        }

        // List of news
        public async Task<IEnumerable<News>> ListOfNews()
        {
            return await db.News.Include(x=>x.Categories).OrderByDescending(x=>x.Id).Take(3).ToListAsync();
        }
    }
}
