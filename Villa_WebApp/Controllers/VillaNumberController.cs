using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Villa_WebApp.Models;
using Villa_WebApp.Models.DTO;
using Villa_WebApp.Models.VM;
using Villa_WebApp.Services.IServices;

namespace Villa_WebApp.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberAPIService _villaNumberAPIService;
        private readonly IVillaAPIService _villaAPIService;
        private readonly ILogger<VillaNumberController> _logger;

        public VillaNumberController(IVillaNumberAPIService villaNumberAPIService, ILogger<VillaNumberController>logger, IVillaAPIService villaAPIService)
        {
            _villaNumberAPIService = villaNumberAPIService;
            _logger = logger;
            _villaAPIService = villaAPIService;
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

        [HttpGet]
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM vm = new VillaNumberCreateVM();
            var response = await _villaAPIService.GetAllAsync<APIResponse>();
            if(response!=null &&response.isSuccess)
            {
                string resultString = Convert.ToString(response.Result);
                var villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(resultString);
                vm.VillaList = villaList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                return View(vm);
            }
            return View("Error");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _villaNumberAPIService.CreateAsync<APIResponse>(vm.VillaNumber);
                    if (response != null && response.isSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
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
