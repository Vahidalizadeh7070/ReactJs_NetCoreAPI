using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Interfaces
{
    // Review interface
    public interface ReviewInterface
    {
        Task<IEnumerable<Review>> GetAll();
        Task<Review> GetReviewById(int id);
        Task<Review> AddReview(Review review);
        Task<IEnumerable<Review>> GetReviewByUserId(string userId);
        Task<IEnumerable<Review>> GetLast4Reviews();
        Task<Review> Delete(int id);
    }
}
