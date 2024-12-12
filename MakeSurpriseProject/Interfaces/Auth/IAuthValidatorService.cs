namespace MakeSurpriseProject.Interfaces.Auth
{
    public interface IAuthValidatorService
    {
        Task<bool> IsEmailRegisteredAsync(string email);
    }
}
