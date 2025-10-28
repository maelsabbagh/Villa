using Villa_WebApp.Models;

namespace Villa_WebApp.Services.IServices
{
    public interface IAPIService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
