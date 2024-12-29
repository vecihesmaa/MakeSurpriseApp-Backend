namespace MakeSurpriseProject.DTOs.UserProfile
{
    public class UserPasswordManagementRequest
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
