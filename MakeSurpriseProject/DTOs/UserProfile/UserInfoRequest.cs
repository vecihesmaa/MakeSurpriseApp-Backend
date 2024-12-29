namespace MakeSurpriseProject.DTOs.UserProfile
{
    public class UserInfoRequest
    { 
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
