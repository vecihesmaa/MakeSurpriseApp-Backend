using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.Entities;
using MakeSurpriseProject.Hubs;
using MakeSurpriseProject.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace MakeSurpriseProject.BackgroundServices
{
    public class SpecialDaysChecker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<SpecialDaysHub> _hubContext;
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;

        public SpecialDaysChecker(IServiceProvider serviceProvider, IHubContext<SpecialDaysHub> hubContext, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _hubContext = hubContext;
            var smtpSettings = configuration.GetSection("SmtpSettings");
            _fromEmail = smtpSettings["FromEmail"];

            _smtpClient = new SmtpClient(smtpSettings["Host"])
            {
                Port = int.Parse(smtpSettings["Port"]),
                Credentials = new NetworkCredential(_fromEmail, smtpSettings["Password"]),
                EnableSsl = true,
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var isNotified = false;
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MakeSurpriseDbContext>();

                var now = DateTime.Now;
                var tomorrow = now.AddDays(1).Date;
                var today = now.Date;

                var specialDays = await dbContext.SpecialDays
                    .Include(s => s.User) 
                    .Where(sd => sd.SpecialDayDate >= today && sd.SpecialDayDate < tomorrow)
                    .ToListAsync();

                foreach (var specialDay in specialDays)
                {
                    var email = specialDay.User?.Email;
                    if (!string.IsNullOrEmpty(email) && !(specialDay.IsNotified ?? false))
                    {
                        await SendSpecialDaysMailAsync(email, specialDay);

                        //if (!string.IsNullOrEmpty(specialDay.Title))
                        //{
                        //    Console.WriteLine("huba girdi");
                        //    await _hubContext.Clients.User(specialDay.User?.UserId.ToString()).SendAsync(
                        //        "receiveNotification",
                        //        $"Yaklaşan özel gün: {specialDay.Title}");
                        //    Console.WriteLine("Notification gonderildi");
                        //}

                        if (!string.IsNullOrEmpty(specialDay.Title))
                        {
                            await _hubContext.Clients.All.SendAsync(
                                "receiveNotification",
                                $"{specialDay.Title}");
                            Console.WriteLine("Notification gönderildi");
                        }
                        specialDay.IsNotified = true;
                        await dbContext.SaveChangesAsync();
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private async Task SendSpecialDaysMailAsync(string toMail, SpecialDay specialDay)
        {
            string emailBody = $@"
            <div style='font-family: Arial, sans-serif; line-height: 1.6;'>
                <div style='text-align: center; margin-bottom: 20px;'>
                    <img src='https://cdn-icons-png.flaticon.com/512/3656/3656855.png' alt='Logo' style='width: 90px; height: 90px; margin-right: 2800px'>
                </div>
                <p>Takviminize eklediğiniz {specialDay.Title} özel günü yaklaşıyor!</p>
                <p> Sevdikleriniz için bu özel günü ve hediye vermeyi unutmayın!</p>
            </div>";

            var sendMailRequest = new SendMailRequest
            {
                ToMail = toMail,
                Subject = "Yaklaşan Özel Gününüz Var!",
                Body = emailBody,
                IsHtml = true
            };
            await SendMail(sendMailRequest);
        }

        private async Task SendMail(SendMailRequest request)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail, "MakeSurprise"),
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
