using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DTOs.Profile;
using MakeSurpriseProject.Entities;
using MakeSurpriseProject.Models.Profile;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.DataAccess
{
    public class ProfileDal
    {
        private readonly MakeSurpriseFinalDbContext _context;

        public ProfileDal(MakeSurpriseFinalDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddProfileAsync(FormAnswer formAnswer, AddProfileRequest profile)
        {
            await _context.FormAnswers.AddAsync(formAnswer);
            await _context.SaveChangesAsync();
            var userRelative = GenerateUserRelativeEntity(formAnswer, profile);    
            await _context.UserRelatives.AddAsync(userRelative);
            await _context.SaveChangesAsync();
            return userRelative.UserRelativeId;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                   .FirstOrDefaultAsync(u => u.Email == email);
        }

        private UserRelative GenerateUserRelativeEntity(FormAnswer formAnswer, AddProfileRequest profile)
        {
            UserRelative userRelative = new UserRelative()
            {
                UserId = profile.ProfileInfo.UserId,
                FirstName = profile.ProfileInfo.FirstName,
                LastName = profile.ProfileInfo.LastName,
                Tag = profile.ProfileInfo.Tag,
                PhoneNumber = profile.ProfileInfo.PhoneNumber,
                UserRelativeType = profile.ProfileInfo.UserRelativeType,
                FormAnswerId = formAnswer.FormAnswerId,
            };
            return userRelative;
        }

        public async Task AddUserRelativeAsync(UserRelative userRelative)
        {
            await _context.UserRelatives.AddAsync(userRelative);
        }

        public async Task<List<QuesitonWithOptionsModel>> GetProfileTestAsync()
        {
            var questionsWithOptions = await _context.FormQuestions
                .Include(q => q.FormOptions)
                .Select(q => new QuesitonWithOptionsModel
                {
                    QuestionId = q.FormQuestionId,
                    QuestionText = q.QuestionText,
                    Options = q.FormOptions.Select(o => new OptionModel
                    {
                        OptionId = o.FormOptionId,
                        OptionText = o.OptionText
                    }).ToList()
                })
                .ToListAsync();

            return questionsWithOptions;
        }

        public async Task<bool> DeleteUserRelativeAsync(int userRelativeId)
        {
            var userRelative = await _context.UserRelatives
                .FirstOrDefaultAsync(ur => ur.UserRelativeId == userRelativeId);
            if (userRelative is null)
                return false;

            _context.UserRelatives.Remove(userRelative);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<UserRelative>> GetAllUserRelativesAsync(GetAllProfilesRequest getAllProfilesModel)
        {
            return await _context.UserRelatives.Where(userRelative => userRelative.UserRelativeType == getAllProfilesModel.UserRelativeType && userRelative.UserId == getAllProfilesModel.UserId).ToListAsync();
        }
    }
}
