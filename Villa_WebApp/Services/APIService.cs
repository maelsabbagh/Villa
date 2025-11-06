using Newtonsoft.Json;
using Villa_WebApp.Models;
using Villa_WebApp.Services.IServices;
using System.Text;
using static Villa_Utility.StaticDetails;
using System.Net;
using System.Net.Http.Headers;

namespace Villa_WebApp.Services
{
    public class APIService : IAPIService
    {
        
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory _httpClient { get; set; }
        private readonly ILogger<APIService> _logger;
        private readonly IHttpContextAccessor _httpContext;
        
        public APIService(IHttpClientFactory httpClientFactory,ILogger<APIService>logger, IHttpContextAccessor httpContext)
        {
            responseModel = new APIResponse();
            _httpClient = httpClientFactory;
            _logger = logger;
            _httpContext = httpContext;

        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("VillaAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.URL);

                if(apiRequest.Data!=null) // post/put request
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),Encoding.UTF8, "application/json");
                }
   
                switch(apiRequest.ApiType)
                {
                    case ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;

                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }

                HttpResponseMessage apiResponse = null;

                string? token = _httpContext?.HttpContext.Session.GetString(SessionToken);
                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                apiResponse = await client.SendAsync(message);
                
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                var errorAPIResponse = new APIResponse()
                {
                    ErrorMessage = new List<string>() { ex.Message },
                    isSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError
                };

                var result = JsonConvert.SerializeObject(errorAPIResponse);
                var APIErrorDesearilized = JsonConvert.DeserializeObject<T>(result);
                return APIErrorDesearilized;
            }
        }
    }
}
