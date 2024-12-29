using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DTOs.UserProfile;
using MakeSurpriseProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.DataAccess
{
    public class EfUserInfoDal
    {
        private readonly MakeSurpriseDbContext _context;

        public EfUserInfoDal(MakeSurpriseDbContext context)
        {
            _context = context;
        }
        public async Task<UserInfoResponse> GetUserInfoAsync(int userId)
        {
            var userInfo = await _context.Users
                            .Where(u => u.UserId == userId)
                            .Select(u => new UserInfoResponse
                            {
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                PhoneNumber = u.UserProfiles.FirstOrDefault().PhoneNumber
                            })
                            .FirstOrDefaultAsync();

            return userInfo;
        }

        public async Task<bool> ChangeUserInfoAsync(UserInfoRequest userInfo)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userInfo.UserId);
            if (user is not null)
            {
                var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(userProfile => userProfile.UserId == user.UserId);
                if (userProfile is null)
                {
                    userProfile = new UserProfile()
                    {
                        UserId = userInfo.UserId,
                    };
                    await _context.UserProfiles.AddAsync(userProfile);
                }
                user.FirstName = userInfo.FirstName;
                user.LastName = userInfo.LastName;
                userProfile.PhoneNumber = userInfo.PhoneNumber;
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }
    }
}
