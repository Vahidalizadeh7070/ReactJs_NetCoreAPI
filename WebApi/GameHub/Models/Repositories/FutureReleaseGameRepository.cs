using GameHub.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Repositories
{
    // Category Repo
    // This class is inherited from FutureReleaseGameInterface
    public class FutureReleaseGameRepository : FutureReleaseGameInterface
    {
        // Require field 
        private readonly AppDbContext appDbContext;

        // Constructor
        public FutureReleaseGameRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        // Get 7 future release games
        public async Task<IEnumerable<FutureReleaseGame>> Get7FutureReleaseGames()
        {
            return await appDbContext.FutureReleaseGames.OrderByDescending(x => x.ReleaseDate).ToListAsync();
        }
    }
}
