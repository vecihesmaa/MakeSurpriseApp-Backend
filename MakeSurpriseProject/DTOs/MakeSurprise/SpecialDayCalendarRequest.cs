namespace MakeSurpriseProject.DTOs.MakeSurprise
{
    public class SpecialDayCalendarRequest
    {
        public DateTime SpecialDayDate { get; set; }

        public string Title { get; set; } = null!;

        public int UserId { get; set; }
    }
}
