using Microsoft.AspNetCore.Identity.Data;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.IRepository
{
    public interface IUserRepository
    {
        Task<bool> isUniqueUser(string userName);
       // Task<LocalUser> Login(LoginRequestDTO loginRequestDTO);
        //Task<LocalUser> Register(LocalUser user);
        Task<ApplicationUser> ValidateUser(string userName, string password);

        Task<ApplicationUser> GetUser(string userName);
    }
}
