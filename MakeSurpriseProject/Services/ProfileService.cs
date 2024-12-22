using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DTOs.Profile;
using MakeSurpriseProject.Entities;
using MakeSurpriseProject.Models.Profile;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.Services
{
    public class ProfileService
    {
        private readonly MakeSurpriseDbContext context;
        public ProfileService(MakeSurpriseDbContext _context)
        {
            context = _context;
        }

        public async Task<int> AddProfileAsync(AddProfileRequest profile)
        {
            FormAnswer formAnswer = new FormAnswer() { 
                FirstQuestionAnswer = profile.ProfileAnswer.FirstQuestionAnswer,
                SecondQuestionAnswer = profile.ProfileAnswer.SecondQuestionAnswer,
                ThirdQuestionAnswer = profile.ProfileAnswer.ThirdQuestionAnswer,
                FourthQuestionAnswer = profile.ProfileAnswer.FourthQuestionAnswer,
                FifthQuestionAnswer = profile.ProfileAnswer.FifthQuestionAnswer,
                SixthQuestionAnswer =   profile.ProfileAnswer.SixthQuestionAnswer,
                SeventhQuestionAnswer = profile.ProfileAnswer.SeventhQuestionAnswer,
                EighthQuestionAnswer = profile.ProfileAnswer.EighthQuestionAnswer,
                NinthQuestionAnswer = profile.ProfileAnswer.NinthQuestionAnswer,
                TenthQuestionAnswer = profile.ProfileAnswer.TenthQuestionAnswer,
                EleventhQuestionAnswer = profile.ProfileAnswer.EleventhQuestionAnswer,
                TwelfthQuestionAnswer = profile.ProfileAnswer.TwelfthQuestionAnswer,
                ThirteenthQuestionAnswer = profile.ProfileAnswer.ThirteenthQuestionAnswer,
                FourteenthQuestionAnswer = profile.ProfileAnswer.FourteenthQuestionAnswer,
                FifteenthQuestionAnswer = profile.ProfileAnswer.FifteenthQuestionAnswer
            };
            await context.FormAnswers.AddAsync(formAnswer);
            await context.SaveChangesAsync();
            UserRelative userRelative = new UserRelative() { 
                UserId = profile.ProfileInfo.UserId,
                FirstName = profile.ProfileInfo.FirstName,
                LastName = profile.ProfileInfo.LastName,
                Tag = profile.ProfileInfo.Tag,
                PhoneNumber = profile.ProfileInfo.PhoneNumber,
                UserRelativeType = profile.ProfileInfo.UserRelativeType,
                FormAnswerId = formAnswer.FormAnswerId,
            };
            await context.UserRelatives.AddAsync(userRelative);
            await context.SaveChangesAsync();
            return userRelative.UserRelativeId;
        }

        public async Task<List<QuesitonWithOptionsModel>> GetProfileTestAsync()
        {
            var questionsWithOptions = await context.FormQuestions
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
            var userRelative = await context.UserRelatives.FindAsync(userRelativeId);
            if (userRelative is null)
                return false;

            context.UserRelatives.Remove(userRelative);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserRelative>> GetAllUserRelativesAsync(GetAllProfilesRequest getAllProfilesModel)
        {
            return await context.UserRelatives.Where(userRelative => userRelative.UserRelativeType == getAllProfilesModel.UserRelativeType && userRelative.UserId == getAllProfilesModel.UserId).ToListAsync();
        }
    }
}
