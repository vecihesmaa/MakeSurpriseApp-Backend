using System.Net.Mail;
using System.Net;
using MakeSurpriseProject.Models;

namespace MakeSurpriseProject.Bussiness
{
    public class MailManager
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;

        public MailManager(IConfiguration configuration)
        {
            var smtpSettings = configuration.GetSection("SmtpSettings");
            _fromEmail = smtpSettings["FromEmail"];

            _smtpClient = new SmtpClient(smtpSettings["Host"])
            {
                Port = int.Parse(smtpSettings["Port"]),
                Credentials = new NetworkCredential(_fromEmail, smtpSettings["Password"]),
                EnableSsl = true,
            };
        }

        public async Task SendEmailAsync(SendMailRequest request)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail, "RequestFlow"),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = request.IsHtml,
            };

            mailMessage.To.Add(request.ToMail);

            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("E-posta başarıyla gönderildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E-posta gönderim hatası: {ex.Message}");
            }
        }
    }
}
