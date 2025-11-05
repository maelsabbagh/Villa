using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Villa_VillaAPI.IRepository;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;
using Villa_VillaAPI.Services.IServices;

namespace Villa_VillaAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _configuration;
        public UserService(ILogger<UserService> logger,IUserRepository userRepository, IConfiguration configuration)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
         
        }
        public async Task<bool> isUniqueUser(string userName)
        {
            return await _userRepository.isUniqueUser(userName);
       
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            bool isValidUser = await _userRepository.isValidUser(loginRequestDTO.UserName, loginRequestDTO.Password);

            if (!isValidUser) throw new UnauthorizedAccessException($"UserName or password are not valid");


            var user = await _userRepository.Login(loginRequestDTO);

            if (user == null) throw new KeyNotFoundException("User not found"); // shouldn't happen
                                                                                
            string writtenToken = GenerateJWTToken(user);


            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = writtenToken,
                User = user
            };
            return loginResponseDTO;
        }

        private string GenerateJWTToken(LocalUser user)
        {
            string secretKey = _configuration.GetValue<string>("ApiSettings:Secret")!;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string writtenToken = tokenHandler.WriteToken(token);
            return writtenToken;
        }

        public async Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            bool uniqueUser = await isUniqueUser(registrationRequestDTO.UserName);
            if(!uniqueUser)
            {
                throw new InvalidOperationException("UserName already exists");
            }
            LocalUser user = new LocalUser()
            {
                Name = registrationRequestDTO.Name,
                Password = registrationRequestDTO.Password,
                UserName = registrationRequestDTO.UserName,
                Role = registrationRequestDTO.Role,
                
            };

            
            return await _userRepository.Register(user);
        }
    }
}
