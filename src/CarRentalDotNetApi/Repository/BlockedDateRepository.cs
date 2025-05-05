using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class BlockedDateRepository : IBlockedDateRepository
    {
         private readonly ApplicationDBContext _context;
        public BlockedDateRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<BlockedDate> CreateAsync(BlockedDate blockedDateModel)
        {
            await _context.BlockedDates.AddAsync(blockedDateModel);
            await _context.SaveChangesAsync();
            return blockedDateModel;
        }

        public async Task<BlockedDate> UpdateAsync(int Id, BlockedDate blockedDateModel)
        {
            var blockedDate = await _context.BlockedDates.FirstOrDefaultAsync(x => x.Id == Id);
            blockedDate.StartDate = blockedDateModel.StartDate;
            blockedDate.EndDate = blockedDateModel.EndDate;
            await _context.SaveChangesAsync();
            return blockedDate;
        }
    }
}