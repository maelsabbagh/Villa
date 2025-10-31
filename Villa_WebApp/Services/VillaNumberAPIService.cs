using Microsoft.Extensions.Configuration;
using Villa_WebApp.Models;
using Villa_WebApp.Models.DTO;
using Villa_WebApp.Services.IServices;
using static Villa_Utility.StaticDetails;

namespace Villa_WebApp.Services
{
    public class VillaNumberAPIService : APIService,IVillaNumberAPIService  
    {
        protected const string apiUrl = "/api/VillaNumberAPI";
        private string backendUrl;
        private readonly IConfiguration _configuration;
        public VillaNumberAPIService(IHttpClientFactory httpClientFactory, ILogger<APIService> logger,IConfiguration configuration) : base(httpClientFactory, logger)
        {
            _configuration = configuration;
            backendUrl = _configuration.GetValue<string>("ServiceUrls:VillaAPI")!;
            backendUrl = backendUrl + apiUrl;
        }

        public async Task<T> CreateAsync<T>(VillaNumberCreateDTO villaNumberDTO)
        {
            var response = await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = villaNumberDTO,
                URL = $"{backendUrl}"
            });
            return response;

        }

        public async Task<T> DeleteAsync<T>(int id)
        {
            var response = await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.DELETE,
                Data = null,
                URL = $"{backendUrl}/{id}"
            });
            return response; 
        }

        public async Task<T> GetAllAsync<T>()
        {
            var response = await SendAsync<T>(new APIRequest
            {
                ApiType = ApiType.GET,
                Data = null,
                URL = $"{backendUrl}"
          });
            return response;

        }

        public async Task<T> GetAsync<T>(int id)
        {
            var response = await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Data = null,
                URL = $"{backendUrl}/{id}"
            });
            return response;
        }

        public async Task<T> UpdateAsync<T>(VillaNumberUpdateDTO villaNumberUpdateDTO)
        {
            var response = await SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.DELETE,
                Data = villaNumberUpdateDTO,
                URL = $"{backendUrl}/{villaNumberUpdateDTO.VillaNo}"
            });
            return response;
        }
    }
}
