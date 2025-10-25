using System.Net;
using Villa_VillaAPI.Models;

namespace Villa_VillaAPI.Services
{
    public interface IAPIService
    {
        APIResponse CreateSuccessResponse(HttpStatusCode statusCode,object? result);
        APIResponse CreateFailureResponse(HttpStatusCode statusCode,List<string>errorMessages);
    }
}
