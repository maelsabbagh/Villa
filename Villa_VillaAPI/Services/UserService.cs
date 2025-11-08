using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Frozen;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserService(ILogger<UserService> logger,IUserRepository userRepository, IConfiguration configuration, UserManager<ApplicationUser> userManager,IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        public async Task<bool> isUniqueUser(string userName)
        {
            return await _userRepository.isUniqueUser(userName);
       
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            ApplicationUser user = await _userRepository.ValidateUser(loginRequestDTO.UserName, loginRequestDTO.Password);

            if (user==null) throw new UnauthorizedAccessException($"UserName or password are not valid");


            //var user = await _userRepository.Login(loginRequestDTO);

            //if (user == null) throw new KeyNotFoundException("User not found"); // shouldn't happen
            string writtenToken =await GenerateJWTToken(user);

            var roles = await _userManager.GetRolesAsync(user);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = writtenToken,
                User = _mapper.Map<UserDTO>(user),
                
            };
            return loginResponseDTO;
        }

        private async Task<string> GenerateJWTToken(ApplicationUser user)
        {
            string secretKey = _configuration.GetValue<string>("ApiSettings:Secret")!;

            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.UserName.ToString()),
                    new Claim(ClaimTypes.Role,roles.FirstOrDefault()!)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string writtenToken = tokenHandler.WriteToken(token);
            return writtenToken;
        }

        public async Task<UserDTO> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            bool uniqueUser = await isUniqueUser(registrationRequestDTO.UserName);
            if(!uniqueUser)
            {
                throw new InvalidOperationException("UserName already exists");
            }
            ApplicationUser user = new ApplicationUser()
            {
                Name = registrationRequestDTO.Name,
                Email = registrationRequestDTO.UserName,
                NormalizedEmail = registrationRequestDTO.UserName.ToUpper(),
                UserName = registrationRequestDTO.UserName,

            };

            
            
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                if(result.Succeeded)
                {
                    await validateRoles();
                    await _userManager.AddToRoleAsync(user, "user");
                    var userToReturn = await _userRepository.GetUser(user.UserName);
                    return new UserDTO()
                    {
                        ID = userToReturn.Id,
                        UserName = userToReturn.UserName,
                        Name = user.Name
                    };
                }
                else
                {
                    string errorMessage = "";
                    foreach (var err in result.Errors.ToList())
                    { 
                        errorMessage = errorMessage + $"{err.Description}\n";
                    }
                

                
                    throw new Exception($"{errorMessage}");
                }  
            


            
        }

        private async Task validateRoles()
        {
            bool isAdminRoleExists=await _roleManager.RoleExistsAsync("admin");
            bool isUserRoleExists = await _roleManager.RoleExistsAsync("user");
            if(!isAdminRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
            }

            if(!isUserRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("user"));
            }


        }
    }
}
