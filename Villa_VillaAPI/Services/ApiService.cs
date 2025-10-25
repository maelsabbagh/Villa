using System.Net;
using Villa_VillaAPI.Models;

namespace Villa_VillaAPI.Services
{
    public class APIService : IAPIService
    {
        public APIResponse CreateFailureResponse(HttpStatusCode statusCode, List<string> errorMessages)
        {
            APIResponse response = new APIResponse()
            {
                isSuccess = false,
                StatusCode = statusCode,
                ErrorMessage = errorMessages,
                Result = null
            };
            return response;
        }

        public APIResponse CreateSuccessResponse(HttpStatusCode statusCode, object? result)
        {
            APIResponse response = new APIResponse()
            {
                isSuccess = true,
                StatusCode = statusCode,
                ErrorMessage = null,
                Result = result
            };
            return response;
        }
    }
}
