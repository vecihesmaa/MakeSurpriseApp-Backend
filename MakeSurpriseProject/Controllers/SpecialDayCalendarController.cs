using MakeSurpriseProject.DTOs.MakeSurprise;
using MakeSurpriseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeSurpriseProject.Controllers
{
    public class SpecialDayCalendarController : Controller
    {
        private readonly SpecialDayCalendarManager specialDayCalendarManager;
        public SpecialDayCalendarController(SpecialDayCalendarManager _specialDayCalendarManager)
        {
            specialDayCalendarManager = _specialDayCalendarManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddSpecialDay([FromBody] SpecialDayCalendarRequest specialDayRequest)
        {
            try
            {
                if (specialDayRequest == null)
                {
                    return BadRequest(new { Message = "Geçersiz istek verisi." });
                }

                var specialDay = await specialDayCalendarManager.AddSpecialDayAsync(specialDayRequest);
                if (specialDay == null)
                {
                    return BadRequest(new { Message = "Özel gün eklenemedi." });
                }

                return Ok(specialDay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Bir hata oluştu.", Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSpecialDay(int specialDayId)
        {
            try
            {
                if (specialDayId == null)
                {
                    return BadRequest(new { Message = "Geçersiz özel gün ID." });
                }

                var isDeleted = await specialDayCalendarManager.DeleteSpecialDayAsync(specialDayId);
                if (isDeleted)
                {
                    return Ok(new { Message = "Başarıyla silindi." });
                }
                else
                {
                    return BadRequest(new { Message = "Silinemedi, lütfen tekrar deneyin." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Bir hata oluştu.", Error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSpecialDays(int userId)
        {
            try
            {
                if (userId == null)
                {
                    return BadRequest(new { Message = "Geçersiz kullanıcı ID." });
                }

                var specialDays = await specialDayCalendarManager.GetAllSpecialDaysAsync(userId);
                if (specialDays == null || !specialDays.Any())
                {
                    return NotFound(new { Message = "Herhangi bir özel gün bulunamadı." });
                }

                return Ok(specialDays);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Bir hata oluştu.", Error = ex.Message });
            }
        }
    }
}
