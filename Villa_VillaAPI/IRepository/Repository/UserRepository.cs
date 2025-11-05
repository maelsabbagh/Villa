using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.IRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LocalUser> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _context.LocalUsers.FirstOrDefaultAsync(u => (u.UserName == loginRequestDTO.UserName && u.Password == loginRequestDTO.Password));
            return user;
        }

        public async Task<bool> isUniqueUser(string userName)
        {
            bool isFound = await _context.LocalUsers.AnyAsync(u => u.UserName == userName);
            return !isFound;

        }

        public async Task<bool> isValidUser(string userName, string password)
        {
            try
            {
                var user = await _context.LocalUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password);
                if (user == null) return false;
                else return true;
            }
            catch(Exception ex)
            {
                return false ;
            }
        }

        //public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<LocalUser> Register(LocalUser user)
        {
            await _context.LocalUsers.AddAsync(user);
            await _context.SaveChangesAsync();

            user.Password = "";
            return user;
        }

        public Task<LocalUser> GetUser(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
