using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHub.Models.Interfaces
{
    // RateList interface
    public interface RateListInterface
    {
        Task<IEnumerable<RateList>> Get7RateList();
    }
}
