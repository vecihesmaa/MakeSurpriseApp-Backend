using MakeSurpriseProject.DTOs.Auth;
using MakeSurpriseProject.Entities;

namespace MakeSurpriseProject.Interfaces.Auth
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest register);

        Task<User> LoginAsync(LoginRequest login);
    }
}
