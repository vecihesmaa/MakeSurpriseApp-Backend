namespace MakeSurpriseProject.Models.Profile
{
    public class QuesitonWithOptionsModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<OptionModel> Options { get; set; }
    }
}
