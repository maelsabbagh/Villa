using Villa_VillaAPI.Models.DTO;
using Villa_VillaAPI.Models;

namespace Villa_VillaAPI.Services.IServices
{
    public interface IUserService
    {
        Task<bool> isUniqueUser(string userName);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
