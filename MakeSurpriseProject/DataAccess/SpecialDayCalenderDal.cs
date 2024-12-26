using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace MakeSurpriseProject.DataAccess
{
    public class SpecialDayCalenderDal
    {
        private readonly MakeSurpriseDbContext _context;
        public SpecialDayCalenderDal(MakeSurpriseDbContext context)
        {
            _context = context;
        }

        public async Task<SpecialDay> AddSpecialDayAsync(SpecialDay specialDay)
        {
            await _context.SpecialDays.AddAsync(specialDay);
            await _context.SaveChangesAsync();
            return specialDay;
        }

        public async Task<SpecialDay> GetSpecialDayByIdAsync(int specialDayId)
        {
            return await _context.SpecialDays.FirstOrDefaultAsync(sd => sd.SpecialDayId == specialDayId);
        }

        public async Task<bool> DeleteSpecialDayAsync(int specialDayId)
        {
            if (specialDayId != null)
            {
                var specialDay = await _context.SpecialDays.FirstOrDefaultAsync((specialDay) => specialDay.SpecialDayId == specialDayId);
                _context.SpecialDays.Remove(specialDay);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<SpecialDay>> GetAllSpecialDaysAsync(int userId)
        {
            if (userId != null)
            {
                var specilDays = await _context.SpecialDays.Where((specialDay) => specialDay.UserId == userId).ToListAsync();
                return specilDays;
            }
            return null;
        }
    }
}
