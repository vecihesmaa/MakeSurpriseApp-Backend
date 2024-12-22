using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DTOs.MakeSurprise;
using MakeSurpriseProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.Services
{
    public class SpecialDayCalendarService
    {
        private readonly MakeSurpriseDbContext context;
        public SpecialDayCalendarService(MakeSurpriseDbContext _context)
        {
            context = _context;
        }

        public async Task<SpecialDay> AddSpecialDayAsync(SpecialDayCalendarRequest specialDay)
        {
            SpecialDay special = new SpecialDay
            {
                SpecialDayDate = specialDay.SpecialDayDate,
                Title = specialDay.Title,
                UserId = specialDay.UserId,
            };
            await context.SpecialDays.AddAsync(special);
            await context.SaveChangesAsync();
            return special;
        }

        public async Task<bool> DeleteSpecialDayAsync(int specialDayId)
        {
            if (specialDayId != null)
            {
                var specialDay = await context.SpecialDays.FirstOrDefaultAsync((specialDay) => specialDay.SpecialDayId == specialDayId);
                context.SpecialDays.Remove(specialDay);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<SpecialDay>> GetAllSpecialDaysAsync(int userId)
        {
            if(userId != null)
            {
                var specilDays = await context.SpecialDays.Where((specialDay) => specialDay.UserId == userId).ToListAsync();
                return specilDays;
            }
            return null;
        }
    }
}
