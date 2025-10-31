using System.Reflection.Metadata.Ecma335;
using Villa_Villa_WebApp.Models.DTO;
using Villa_WebApp.Models.DTO;
using Villa_WebApp.Services.IServices;
using static Villa_Utility.StaticDetails;

namespace Villa_WebApp.Services
{
    public class VillaAPIService : APIService, IVillaAPIService
    {
        const string apiUrl = "/api/VillaAPI";
        private string backendUrl;
        private readonly IConfiguration _configuration;
        public VillaAPIService(IHttpClientFactory httpClientFactory, ILogger<APIService> logger,IConfiguration configuration) : base(httpClientFactory, logger)
        {
            _configuration = configuration;
            backendUrl = _configuration.GetValue<string>("ServiceUrls:VillaAPI")!;
            backendUrl = backendUrl + apiUrl;
        }

        public async Task<T> CreateAsync<T>(VillaCreateDTO villaCreateDTO)
        {
           return await SendAsync<T>(new Models.APIRequest()
            {
                ApiType = ApiType.POST,
                URL=backendUrl,
                Data = villaCreateDTO
            });
        }

        public async Task<T> DeleteAsync<T>(int id)
        {
            return await SendAsync<T>(new Models.APIRequest()
            {
                ApiType = ApiType.DELETE,
                URL = $"{backendUrl} +/{id}",
                Data = null
            });
        }

        public async Task<T> GetAllAsync<T>()
        {

            var response = await SendAsync<T>(new Models.APIRequest()
            {
                ApiType = ApiType.GET,
                URL = backendUrl,
                Data = null
            });
            return response;
        }

        public async Task<T> GetAsync<T>(int id)
        {
            return await SendAsync<T>(new Models.APIRequest()
            {
                ApiType = ApiType.GET,
                URL = $"{backendUrl}/{id}",
                Data = null
            });
        }

        public async Task<T> UpdateAsync<T>(VillaUpdateDTO villaUpdateDTO)
        {
            var response =  await SendAsync<T>(new Models.APIRequest()
            {
                ApiType = ApiType.PUT,
                URL = $"{backendUrl}/{villaUpdateDTO.Id}",
                Data = villaUpdateDTO
            });
            return response;
        }
    }
}
