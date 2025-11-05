using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Villa_Villa_WebApp.Models.DTO;
using Villa_WebApp.Models;
using Villa_WebApp.Services.IServices;
using static Villa_Utility.StaticDetails;

namespace Villa_WebApp.Services
{
    public class AuthService : APIService,IAuthService
    {
        private string backendUrl;
        private readonly IConfiguration _configuration;
        private string apiUrl = "/api/User";
        public AuthService(IHttpClientFactory httpClientFactory, ILogger<APIService> logger,IConfiguration configuration) : base(httpClientFactory, logger)
        {
            _configuration = configuration;
            backendUrl = _configuration.GetValue<string>("ServiceUrls:VillaAPI")!;
            backendUrl = backendUrl + apiUrl;
        }

        public async Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = loginRequestDTO,
                URL = $"backendUrl/login"
            };

           return await SendAsync<T>(apiRequest);
        }

        public async Task<T> RegisterAsync<T>(RegistrationRequestDTO registrationRequestDTO)
        {
            APIRequest apiRequest = new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = registrationRequestDTO,
                URL = $"backendUrl/register"
            };

            return await SendAsync<T>(apiRequest);
        }
    }
}
