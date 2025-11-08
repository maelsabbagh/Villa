using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.IRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //public async Task<LocalUser> Login(LoginRequestDTO loginRequestDTO)
        //{
        //    var user = await _context.LocalUsers.FirstOrDefaultAsync(u => (u.UserName == loginRequestDTO.UserName && u.Password == loginRequestDTO.Password));
        //    user!.Password = "";
        //    return user;
        //}

        public async Task<bool> isUniqueUser(string userName)
        {
            bool isFound = await _context.ApplicationUsers.AnyAsync(u => u.UserName == userName);
            return !isFound;

        }

        public async Task<ApplicationUser> ValidateUser(string userName, string password)
        {
            try
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
                if (user == null) return null;

                bool isValidPassword = await _userManager.CheckPasswordAsync(user, password);
                if (isValidPassword) return user;
                return null;
               
            }
            catch(Exception ex)
            {
                return null ;
            }
        }

        //public async Task<LocalUser> Register(LocalUser user)
        //{
        //    await _context.LocalUsers.AddAsync(user);
        //    await _context.SaveChangesAsync();

        //    user.Password = "";
        //    return user;
        //}

        public async Task<ApplicationUser> GetUser(string userName)
        {
            return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower());
        }
    }
}
