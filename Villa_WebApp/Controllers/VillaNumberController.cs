using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Villa_WebApp.Models;
using Villa_WebApp.Models.DTO;
using Villa_WebApp.Services.IServices;

namespace Villa_WebApp.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberAPIService _villaNumberAPIService;
        private readonly ILogger<VillaNumberController> _logger;

        public VillaNumberController(IVillaNumberAPIService villaNumberAPIService, ILogger<VillaNumberController>logger)
        {
            _villaNumberAPIService = villaNumberAPIService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var apiResponse = await _villaNumberAPIService.GetAllAsync<APIResponse>();
                if (apiResponse != null && apiResponse.isSuccess)
                {
                    string resultString = Convert.ToString(apiResponse.Result);
                    var villaNumbers = JsonConvert.DeserializeObject<IEnumerable<VillaNumberDTO>>(resultString);
                    return View(villaNumbers);
                }

                return View("Error");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }
    }
}
