using MakeSurpriseProject.DTOs.Profile;
using MakeSurpriseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeSurpriseProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileManager profileManager;

        public ProfileController(ProfileManager _profileManager)
        {
            profileManager = _profileManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddProfile([FromBody] AddProfileRequest profile)
        {
            try
            {
                if (profile == null)
                {
                    return BadRequest(new { Message = "Geçersiz istek verisi. Profil bilgileri eksik." });
                }

                int userRelativeId = await profileManager.AddProfileAsync(profile);
                if (userRelativeId == null)
                {
                    return BadRequest(new { Message = "Profil eklenemedi. Lütfen tekrar deneyiniz." });
                }

                return Ok(new { UserRelativeId = userRelativeId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Bir hata oluştu.", Error = ex.Message });
            }
        }

        public async Task<IActionResult> DeleteProfile(int userRelativeId)
        {
            try
            {
                if (userRelativeId == null)
                {
                    return BadRequest(new { Message = "Geçersiz kullanıcı yakını ID." });
                }

                bool isDeleted = await profileManager.DeleteUserRelativeAsync(userRelativeId);
                if (isDeleted)
                {
                    return Ok(new { Message = "Profil başarıyla silindi." });
                }
                else
                {
                    return BadRequest(new { Message = "Profil silinemedi. Lütfen tekrar deneyiniz." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Bir hata oluştu.", Error = ex.Message });
            }
        }
        public async Task<IActionResult> GetProfileTest()
        {
            try
            {
                var profileTest = await profileManager.GetProfileTestAsync();
                if (profileTest == null)
                {
                    return NotFound(new { Message = "Profil testi bulunamadı." });
                }

                return Ok(profileTest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Bir hata oluştu.", Error = ex.Message });
            }
        }
        public async Task<IActionResult> GetAllProfiles(GetAllProfilesRequest getAllProfilesModel)
        {
            try
            {
                if (getAllProfilesModel == null)
                {
                    return BadRequest(new { Message = "Geçersiz istek verisi. Tüm profilleri getirmek için gerekli bilgiler eksik." });
                }

                var profiles = await profileManager.GetAllUserRelativesAsync(getAllProfilesModel);
                if (profiles == null || !profiles.Any())
                {
                    return NotFound(new { Message = "Herhangi bir profil bulunamadı." });
                }

                return Ok(profiles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Bir hata oluştu.", Error = ex.Message });
            }
        }
    }
}
