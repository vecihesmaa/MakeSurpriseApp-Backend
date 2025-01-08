using MakeSurpriseProject.DTOs.UserProfile;
using MakeSurpriseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeSurpriseProject.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly CargoTrackingManager cargoTrackingManager;
        private readonly ProfileManager profileManager;
        private readonly UserInfoManager userInfoManager;
        //private readonly UserPasswordService userPasswordService;
        //private readonly UserInfoService userInfoService;

        public UserProfileController(CargoTrackingManager _cargoTrackingManager, ProfileManager _profileManager, UserInfoManager _userInfoManager)
        {
            cargoTrackingManager = _cargoTrackingManager;
            profileManager = _profileManager;
            userInfoManager = _userInfoManager;
            //userPasswordService = _userPasswordService;
            //userInfoService = _userInfoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCargos(int userId)
        {
            var orderItems = await cargoTrackingManager.GetAllCargosAsync(userId);
            return Ok(orderItems);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] UserPasswordManagementRequest userPasswordManagement)
        {
            var isOldPasswordTrue = await profileManager.IsPasswordRegisteredAsync(userPasswordManagement);
            if (isOldPasswordTrue)
            {
                var isSucceedChangePassword = await profileManager.ChangePasswordAsync(userPasswordManagement);
                if (isSucceedChangePassword)
                {
                    return Ok("");
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] UserPasswordReset userPasswordReset)
        {
            var isSucceedChangePassword = await profileManager.ResetPasswordAsync(userPasswordReset);
            if (isSucceedChangePassword)
            {
                return Ok("");
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfo(int userId)
        {
            var userInfo = await userInfoManager.GetUserInfoAsync(userId);
            return Ok(userInfo);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserInfo([FromBody] UserInfoRequest userInfo)
        {
            var isSucceedChangeUserInfo = await userInfoManager.ChangeUserInfoAsync(userInfo);
            if (isSucceedChangeUserInfo)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
