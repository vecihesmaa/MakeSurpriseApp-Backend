using MakeSurpriseProject.DTOs.Profile;
using MakeSurpriseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeSurpriseProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ProfileService profileService;

        public ProfileController(ProfileService _profileService)
        {
            profileService = _profileService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProfile([FromBody]AddProfileRequest profile)
        {
            int userRelativeId = await profileService.AddProfileAsync(profile);
            return Ok(userRelativeId);
        }

        public async Task<IActionResult> DeleteProfile(int userRelativeId)
        {
            if(userRelativeId != null)
            {
                bool isDeleted = await profileService.DeleteUserRelativeAsync(userRelativeId);
                if (isDeleted)
                {
                    return Ok(new { Message = "Your profile has been successfully deleted." });
                }
            }
            return BadRequest(new { Message = "An error occurred while deleting the profile. Please try again." });
        }

        public async Task<IActionResult> GetProfileTest()
        {
            var profileTest = await profileService.GetProfileTestAsync();
            return Ok(profileTest);
        }
        public async Task<IActionResult> GetAllProfiles(GetAllProfilesRequest getAllProfilesModel)
        {
            if(getAllProfilesModel != null)
            {
                var profiles = await profileService.GetAllUserRelativesAsync(getAllProfilesModel);
                return Ok(profiles);
            }
            return BadRequest(new { Message = "An error occurred while deleting the profile. Please try again." });
        }
    }
}
