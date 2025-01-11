using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DataAccess;
using MakeSurpriseProject.DTOs.MakeSurprise;
using MakeSurpriseProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.Services
{
    public class SpecialDayCalendarManager
    {
        private readonly SpecialDayCalenderDal _specialDayCalenderDal;
        public SpecialDayCalendarManager(MakeSurpriseFinalDbContext _context, SpecialDayCalenderDal specialDayCalenderDal)
        {
            _specialDayCalenderDal = specialDayCalenderDal;
        }

        public async Task<SpecialDay> AddSpecialDayAsync(SpecialDayCalendarRequest specialDayDto)
        {
            SpecialDay specialDay = new SpecialDay
            {
                SpecialDayDate = specialDayDto.SpecialDayDate,
                Title = specialDayDto.Title,
                UserId = specialDayDto.UserId,
                IsNotified = false
            };
            var result = await _specialDayCalenderDal.AddSpecialDayAsync(specialDay);
            return result;
        }

        public async Task<bool> DeleteSpecialDayAsync(int specialDayId)
        {
           var result = await _specialDayCalenderDal.DeleteSpecialDayAsync(specialDayId);
            return result;
        }

        public async Task<List<SpecialDay>> GetAllSpecialDaysAsync(int userId)
        {
            var result = await _specialDayCalenderDal.GetAllSpecialDaysAsync(userId);
            return result;
        }
    }
}
