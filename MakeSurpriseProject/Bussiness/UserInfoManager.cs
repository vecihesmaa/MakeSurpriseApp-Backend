using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DataAccess;
using MakeSurpriseProject.DTOs.UserProfile;
using MakeSurpriseProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.Services
{
    public class UserInfoManager
    {
        private readonly EfUserInfoDal _efUserInfoDal;
        public UserInfoManager(EfUserInfoDal efUserInfoDal)
        {
            _efUserInfoDal = efUserInfoDal;
        }

        public async Task<UserInfoResponse> GetUserInfoAsync(int userId)
        {
            var result = await _efUserInfoDal.GetUserInfoAsync(userId);
            return result;
        }

        public async Task<bool> ChangeUserInfoAsync(UserInfoRequest userInfo)
        {
            var result = await _efUserInfoDal.ChangeUserInfoAsync(userInfo);
            return false;
        }
    }
}
