using GameHub.Areas.Dashboard.Models.Interfaces;
using GameHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Areas.Dashboard.Models.Repositories
{
    // This repository inherits from ManagerNewsInterface
    public class ManagerNewsRepository : ManagerNewsInterface
    {
        private readonly AppDbContext appDbContext;

        // DbContext field
        public ManagerNewsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // Add News
        public async Task<News> AddNews(News news)
        {
            var result = await appDbContext.News.AddAsync(news);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Delete News
        public async Task<News> Delete(int id)
        {
            var result = await appDbContext.News.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                appDbContext.News.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        // Edit News
        public async Task<News> EditNews(News news)
        {
            if (news != null)
            {
                var newss = appDbContext.News.Attach(news);
                newss.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await appDbContext.SaveChangesAsync();
                return news;

            }
            return null;
        }

        // Get all news 
        public async Task<IEnumerable<News>> GetAll()
        {
            return await appDbContext.News.Include(x=>x.Categories).OrderByDescending(x => x.Id).ToListAsync();
        }

        // Get a specific news by id
        public async Task<News> GetNewsById(int id)
        {
            return await appDbContext.News.Include(x=>x.Categories).OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
