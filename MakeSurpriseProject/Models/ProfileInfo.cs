namespace MakeSurpriseProject.Models
{
    public class ProfileInfo
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string Tag { get; set; } = null!;

        public bool? UserRelativeType { get; set; }
    }
}
