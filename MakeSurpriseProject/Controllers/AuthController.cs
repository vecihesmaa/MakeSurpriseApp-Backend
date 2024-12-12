using FluentValidation;
using MakeSurpriseProject.DTOs.Auth;
using MakeSurpriseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeSurpriseProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService authService;
        private readonly IValidator<RegisterRequest> registerValidator;
        private readonly IValidator<LoginRequest> loginValidator;
        public AuthController(AuthService _authService, IValidator<RegisterRequest> _registerValidator, IValidator<LoginRequest> _loginValidator)
        {
            authService = _authService;
            registerValidator = _registerValidator;
            loginValidator = _loginValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var validationResult = await registerValidator.ValidateAsync(registerRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { Field = e.PropertyName, Error = e.ErrorMessage }));
            }

            if (await authService.IsEmailRegisteredAsync(registerRequest.Email))
            {
                return Conflict(new { Message = "This email is already registered." });
            }

            await authService.RegisterAsync(registerRequest);
            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var validationResult = await loginValidator.ValidateAsync(loginRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { Field = e.PropertyName, Error = e.ErrorMessage }));
            }

            var loginData = await authService.LoginAsync(loginRequest);

            if (loginData is null)
            {
                return Conflict(new { Message = "Invalid email or password" });
            }


            return Ok(new { Message = "Login successful", User = new { loginData.UserId } });
        }
    }
}
