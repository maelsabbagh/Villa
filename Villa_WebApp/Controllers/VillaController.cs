using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_WebApp.Models;
using Villa_WebApp.Models.DTO;
using Villa_WebApp.Services.IServices;

namespace Villa_WebApp.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaAPIService _villaAPIService;
        private readonly ILogger<VillaController> _logger;
        private readonly IMapper _mapper;

        public VillaController(IVillaAPIService villaAPIService,ILogger<VillaController>logger,IMapper mapper)
        {
            _villaAPIService = villaAPIService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task <IActionResult> Index()
        {
            try
            {
                List<VillaDTO> villaList = new List<VillaDTO>();
                var response = await _villaAPIService.GetAllAsync<APIResponse>();

                if (response != null && response.isSuccess)
                {
                    villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
                }
                return View(villaList);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }
    }
}
