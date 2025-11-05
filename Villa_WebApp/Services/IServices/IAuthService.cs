using Villa_Villa_WebApp.Models.DTO;

namespace Villa_WebApp.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO);
        Task<T> RegisterAsync<T>(RegistrationRequestDTO registrationRequestDTO);
    }
}
