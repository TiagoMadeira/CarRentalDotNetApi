using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IBlockedDateRepository
    {
    
        Task<BlockedDate> CreateAsync(BlockedDate blockedDateModel);

        Task<BlockedDate> UpdateAsync(int Id, BlockedDate blockedDateModel);
    }
    }
