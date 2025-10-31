using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_Villa_WebApp.Models.DTO;
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

        public VillaController(IVillaAPIService villaAPIService, ILogger<VillaController> logger, IMapper mapper)
        {
            _villaAPIService = villaAPIService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
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
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateVilla()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO villaCreateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(villaCreateDTO);
                }

                var apiResponse = await _villaAPIService.CreateAsync<APIResponse>(villaCreateDTO);
                if (apiResponse != null && apiResponse.isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    return View(villaCreateDTO);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            try
            {
                if (villaId <= 0) return View("Error");
                VillaDTO villaDTO = null;
                VillaUpdateDTO villaUpdate = null;
                var response = await _villaAPIService.GetAsync<APIResponse>(villaId);
                if (response != null && response.isSuccess)
                {
                    string responseString = Convert.ToString(response.Result);
                    villaDTO = JsonConvert.DeserializeObject<VillaDTO>(responseString);
                     villaUpdate =  _mapper.Map<VillaUpdateDTO>(villaDTO);
                }
                else
                {
                    string errorLog = $"update villa in villa Controller received error response";
                    _logger.LogError("errorLog");
                    return View("Error");
                }

                return View(villaUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO villaDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var apiResponse = await _villaAPIService.UpdateAsync<APIResponse>(villaDto);
                    if (apiResponse != null && apiResponse.isSuccess)
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

        [HttpGet]
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _villaAPIService.GetAsync<APIResponse>(villaId);
            VillaDTO villaDTO = null;
            if (response != null && response.isSuccess)
            {
                string responseString = Convert.ToString(response.Result);
                villaDTO = JsonConvert.DeserializeObject<VillaDTO>(responseString);
                return View(villaDTO);
            }
            else
            {
                string errorLog = $"delete villa in villa Controller received error response";
                _logger.LogError("errorLog");
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult>DeleteVilla(VillaDTO villaDTO)
        {
            int villaId = villaDTO.Id;
            if (villaId <= 0) return View("Error");
            var response =await _villaAPIService.DeleteAsync<APIResponse>(villaId);
            if(response!=null &&response.isSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Error");
        }
    }
}
