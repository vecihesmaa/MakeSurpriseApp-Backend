using System.Net.Mail;
using System.Net;
using MakeSurpriseProject.Models;
using Microsoft.Extensions.Caching.Memory;
using MakeSurpriseProject.DataAccess;

namespace MakeSurpriseProject.Bussiness
{
    public class MailManager
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;
        private readonly IMemoryCache _memoryCache;
        private readonly ProfileDal _profileDal;
        public MailManager(IConfiguration configuration, IMemoryCache memoryCache, ProfileDal profileDal)
        {
            _memoryCache = memoryCache;
            _profileDal = profileDal;
            var smtpSettings = configuration.GetSection("SmtpSettings");
            _fromEmail = smtpSettings["FromEmail"];

            _smtpClient = new SmtpClient(smtpSettings["Host"])
            {
                Port = int.Parse(smtpSettings["Port"]),
                Credentials = new NetworkCredential(_fromEmail, smtpSettings["Password"]),
                EnableSsl = true,
            };
        }

        public async Task SendMail(SendMailRequest request)
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

        public async Task<string> SendChangePasswordMailAsync(string toMail)
        {
            var user = await _profileDal.GetUserByEmailAsync(toMail);
            if (user == null)
            {
                var errorMessage = "Sistemde böyle bir e-posta adresi bulunmamaktadır. Lütfen geçerli bir e-posta adresi girin.";
                return errorMessage;
            }
            else
            {
                Random generator = new Random();
                string randomCode = generator.Next(100000, 1000000).ToString();

                string emailBody = $@"
                    <div style='font-family: Arial, sans-serif; line-height: 1.6; padding: 20px;'>
                        <div style='text-align: center; margin-bottom: 20px;'>
                            <img src='https://cdn-icons-png.flaticon.com/512/3656/3656855.png' 
                                 alt='Logo' 
                                 style='width: 150px; height: 150px; margin-bottom: 10px;'>
                        </div>
                        <p style='font-size: 16px; text-align: center; color: #333;'>
                            <strong>Merhaba,</strong><br><br>
                            Şifrenizi değiştirmek için aşağıdaki kodu kullanabilirsiniz:
                        </p>
                        <p style='font-size: 20px; font-weight: bold; text-align: center; color: #FF5733;'>
                            {randomCode}
                        </p>
                        <p style='font-size: 14px; text-align: center; color: #555;'>
                            Bu işlem isteğiniz dışında gerçekleştiyse lütfen bizimle mail adresimiz aracılığı ile iletişime geçiniz.
                        </p>
                        <p style='font-size: 14px; text-align: center; color: #333;'>
                            İyi günler dileriz,<br>
                            <strong>MakeSurprise Ekibi</strong>
                        </p>
                    </div>";

                var sendMailRequest = new SendMailRequest
                {
                    ToMail = toMail,
                    Subject = "Şifre Değiştirme Talebi",
                    Body = emailBody,
                    IsHtml = true
                };
                SendMail(sendMailRequest);
                _memoryCache.Set(toMail, randomCode, TimeSpan.FromMinutes(10));
                return randomCode;
            }
        }
        public async Task<bool> VerifyResetCode(string toMail, string code)
        {
            if (_memoryCache.TryGetValue(toMail, out string storedCode))
            {
                return storedCode == code;
            }
            return false;
        }

    }
}
