using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Interfaces
{
    // News interface
    public interface NewsInterface
    {
        Task<IEnumerable<News>> ListOfNews();
        Task<News> GetNewsById(int id);
        Task<IList<News>> GetNewsByCategoryId(int cateporyId);
        Task<IList<News>> GetNewsByTrend();

    }
}
