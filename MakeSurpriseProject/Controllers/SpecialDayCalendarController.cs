using MakeSurpriseProject.DTOs.MakeSurprise;
using MakeSurpriseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeSurpriseProject.Controllers
{
    public class SpecialDayCalendarController : Controller
    {
        private readonly SpecialDayCalendarService specialDayCalendarService;
        public SpecialDayCalendarController(SpecialDayCalendarService _specialDayCalendarService)
        {
            specialDayCalendarService = _specialDayCalendarService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSpecialDay([FromBody] SpecialDayCalendarRequest specialDayRequest)
        {
            var specialDay = await specialDayCalendarService.AddSpecialDayAsync(specialDayRequest);
            return Ok(specialDay);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSpecialDay(int specialDayId)
        {
            var isDeleted = await specialDayCalendarService.DeleteSpecialDayAsync(specialDayId);
            if (isDeleted)
            {
                return Ok(new { Message = "Başarıyla Silindi" });
            }
            else
            {
                return BadRequest(new { Message = "SİLİNEMEDİ" });
            }
        }

        public async Task<IActionResult> GetAllSpecialDays(int userId)
        {
            var specialDays = await specialDayCalendarService.GetAllSpecialDaysAsync(userId);

            return Ok(specialDays);

        }
    }
}
