using MakeSurpriseProject.Bussiness;
using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DTOs.UserProfile;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.DataAccess
{
    public class EfUserProfileDal
    {
        private readonly MakeSurpriseDbContext _context;

        public EfUserProfileDal(MakeSurpriseDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsPasswordRegisteredAsync(UserPasswordManagementRequest userPasswordManagement)
        {
            return await _context.Users.AnyAsync(user => user.UserId == userPasswordManagement.UserId && user.Password == userPasswordManagement.OldPassword);
        }

        public async Task<bool> ChangePasswordAsync(UserPasswordManagementRequest userPasswordManagement)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userPasswordManagement.UserId);
            if (user is not null)
            {
                user.Password = userPasswordManagement.NewPassword;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
