using static Villa_Utility.StaticDetails;

namespace Villa_WebApp.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string URL { get; set; }
        public object Data { get; set; }
    } 
}
