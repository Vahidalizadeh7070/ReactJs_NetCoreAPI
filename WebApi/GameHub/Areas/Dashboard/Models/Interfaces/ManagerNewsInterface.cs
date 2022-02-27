using GameHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Areas.Dashboard.Models.Interfaces
{
    // This interface is responsible for CRUD operation
    // The related repository will be inherited from it
    public interface ManagerNewsInterface
    {
        Task<IEnumerable<News>> GetAll();
        Task<News> GetNewsById(int id);
        Task<News> AddNews(News news);
        Task<News> EditNews(News news);
        Task<News> Delete(int id);
    }
}
