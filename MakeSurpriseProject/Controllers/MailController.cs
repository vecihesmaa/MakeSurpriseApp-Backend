using MakeSurpriseProject.Bussiness;
using MakeSurpriseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MakeSurpriseProject.Controllers
{
    public class MailController : Controller
    {
        private readonly MailManager _mailManager;

        public MailController(MailManager mailManager)
        {
            _mailManager = mailManager;
        }

        [HttpPost]
        public async Task<string> SendVerificationCode([FromBody] SendMailRequest request)
        {
            var result = await _mailManager.SendChangePasswordMailAsync(request.ToMail);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> VerifyResetCode([FromBody] VerifyResetCodeRequest request)
        {

            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Code))
            {
                return BadRequest(new { message = "E-posta adresi veya doğrulama kodu eksik." });
            }

            bool isCodeValid = await _mailManager.VerifyResetCode(request.Email, request.Code);

            if (isCodeValid)
            {
                return Ok(new { message = "Doğrulama kodu doğru." });
            }
            else
            {
                return BadRequest(new { message = "Doğrulama kodu yanlış." });
            }
        }

    }
}
