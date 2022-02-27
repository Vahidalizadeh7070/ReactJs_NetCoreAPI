using GameHub.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Repositories
{
    // Review Repo
    // This class is inherited from ReviewInterface
    public class ReviewRepository : ReviewInterface
    {
        // Require field 
        private readonly AppDbContext appDbContext;

        // Constructor
        public ReviewRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // Add a review
        public async Task<Review> AddReview(Review review)
        {
            var result = await appDbContext.Review.AddAsync(review);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Delete a review
        public async Task<Review> Delete(int id)
        {
            var result = await appDbContext.Review.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                appDbContext.Review.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        // Get all reviews
        public async Task<IEnumerable<Review>> GetAll()
        {
            return await appDbContext.Review.Include(x => x.Category).Include(x=>x.User).OrderByDescending(x => x.Id).ToListAsync();
        }

        // Get a review by id
        public async Task<Review> GetReviewById(int id)
        {
            return await appDbContext.Review.Include(x => x.Category).Include(x => x.User).OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Id == id);
        }

        // Get a review by user id
        public async Task<IEnumerable<Review>> GetReviewByUserId(string userId)
        {
            return await appDbContext.Review.Include(x=>x.Category).Where(x=>x.UserId==userId).OrderByDescending(x => x.Id).Take(8).ToListAsync();
        }

        // Get last 4 reviews
        public async Task<IEnumerable<Review>> GetLast4Reviews()
        {
            return await appDbContext.Review.Include(x => x.Category).Include(x => x.User).OrderByDescending(x => x.Id).Take(4).ToListAsync();
        }
    }
}
