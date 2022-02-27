using GameHub.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Repositories
{
    // RateList Repo
    // This class is inherited from RateListInterface
    public class RateListRepository : RateListInterface
    {
        // Require field 
        private readonly AppDbContext appDbContext;

        // Constructor
        public RateListRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        // Get 7 rate list
        public async Task<IEnumerable<RateList>> Get7RateList()
        {
            return await appDbContext.RateList.OrderByDescending(x => x.Id).ToListAsync();
        }
    }
}
