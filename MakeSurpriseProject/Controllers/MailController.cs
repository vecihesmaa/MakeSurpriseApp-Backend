using MakeSurpriseProject.Bussiness;
using MakeSurpriseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MakeSurpriseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly MailManager mailManager;

        public MailController(MailManager mailManager)
        {
            this.mailManager = mailManager;
        }

        [HttpPost("SendEmail")]
        public Task SendEmailAsync([FromBody] SendMailRequest request)
        {
            return mailManager.SendEmailAsync(request);
        }
    }
}
