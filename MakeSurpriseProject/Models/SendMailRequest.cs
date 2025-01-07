namespace MakeSurpriseProject.Models
{
    public class SendMailRequest
    {
        public string ToMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}
