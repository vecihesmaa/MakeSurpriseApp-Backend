using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DataAccess;
using MakeSurpriseProject.DTOs.Profile;
using MakeSurpriseProject.Entities;
using MakeSurpriseProject.Models.Profile;

namespace MakeSurpriseProject.Services
{
    public class ProfileManager
    {
        private readonly ProfileDal _profileDal;
        public ProfileManager(MakeSurpriseDbContext _context, ProfileDal profileDal)
        {
            _profileDal = profileDal;
        }

        public async Task<int> AddProfileAsync(AddProfileRequest profile)
        {
            var formAnswer = GenerateFormAnswers(profile);
            var result = await _profileDal.AddProfileAsync(formAnswer, profile);
            return result;
        }

        private FormAnswer GenerateFormAnswers(AddProfileRequest profile)
        {
            FormAnswer formAnswer = new FormAnswer()
            {
                FirstQuestionAnswer = profile.ProfileAnswer.FirstQuestionAnswer,
                SecondQuestionAnswer = profile.ProfileAnswer.SecondQuestionAnswer,
                ThirdQuestionAnswer = profile.ProfileAnswer.ThirdQuestionAnswer,
                FourthQuestionAnswer = profile.ProfileAnswer.FourthQuestionAnswer,
                FifthQuestionAnswer = profile.ProfileAnswer.FifthQuestionAnswer,
                SixthQuestionAnswer = profile.ProfileAnswer.SixthQuestionAnswer,
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
            return formAnswer;
        }

        public async Task<List<QuesitonWithOptionsModel>> GetProfileTestAsync()
        {
            var questionsWithOptions = await _profileDal.GetProfileTestAsync();
            return questionsWithOptions;
        }

        public async Task<bool> DeleteUserRelativeAsync(int userRelativeId)
        {
            var result = await _profileDal.DeleteUserRelativeAsync(userRelativeId);
            return result;
        }

        public async Task<List<UserRelative>> GetAllUserRelativesAsync(GetAllProfilesRequest getAllProfilesModel)
        {
            var result = await _profileDal.GetAllUserRelativesAsync(getAllProfilesModel);
            return result;
        }
    }
}
