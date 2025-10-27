using System.Net;

namespace Villa_WebApp.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool isSuccess { get; set; }
        public List<string>? ErrorMessage { get; set; }
        public object? Result { get; set; }
    }
}
