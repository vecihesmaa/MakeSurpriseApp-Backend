using MakeSurpriseProject.Models;

namespace MakeSurpriseProject.DTOs.Profile
{
    public class AddProfileRequest
    {
        public ProfileInfo ProfileInfo { get; set; }
        public ProfileAnswer ProfileAnswer { get; set; }
    }
}
