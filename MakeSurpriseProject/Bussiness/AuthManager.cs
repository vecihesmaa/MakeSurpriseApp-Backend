using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DTOs.Auth;
using MakeSurpriseProject.Entities;
using MakeSurpriseProject.Interfaces.Auth;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.Services
{
    public class AuthManager : IAuthService, IAuthValidatorService
    {
        private readonly MakeSurpriseDbContext context;
        public AuthManager(MakeSurpriseDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            return await context.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<User> LoginAsync(LoginRequest login)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Email == login.Email && user.Password == login.Password);
        }

        public async Task RegisterAsync(RegisterRequest register)
        {
            var user = new User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                Password = register.Password,
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
    }
}
