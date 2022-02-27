using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Interfaces
{
    // FutureReleaseGame interface
    public interface FutureReleaseGameInterface
    {
        Task<IEnumerable<FutureReleaseGame>> Get7FutureReleaseGames();
    }
}
