using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
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
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberAPIService villaNumberAPIService, ILogger<VillaNumberController>logger, IVillaAPIService villaAPIService,IMapper mapper)
        {
            _villaNumberAPIService = villaNumberAPIService;
            _logger = logger;
            _villaAPIService = villaAPIService;
            _mapper = mapper;
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
                    else
                    {
                        if(response.ErrorMessage.Count>0)
                        {
                            ModelState.AddModelError("Error", response.ErrorMessage.FirstOrDefault());
                        }
                        // Villa List in vm is not populated
                        // we need to populate it when displaying an error 
                        // so user can re-select

                        var villaResponse = await _villaAPIService.GetAllAsync<APIResponse>();
                        if (villaResponse != null && villaResponse.isSuccess)
                        {
                            string resultString = Convert.ToString(villaResponse.Result);
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
        public async Task<IActionResult>UpdateVillaNumber(int villaNo)
        {
            try
            {
                VillaNumberUpdateVM vm = new VillaNumberUpdateVM();    
                var apiResponseGetVillaNumber = await _villaNumberAPIService.GetAsync<APIResponse>(villaNo);
                if (apiResponseGetVillaNumber != null && apiResponseGetVillaNumber.isSuccess)
                {
                    string apiResultString = Convert.ToString(apiResponseGetVillaNumber.Result)!;
                    VillaNumberDTO villaNumber = JsonConvert.DeserializeObject<VillaNumberDTO>(apiResultString)!;

                    VillaNumberUpdateDTO villaNumberUpdateDTO = _mapper.Map<VillaNumberUpdateDTO>(villaNumber);

                    var apiResponseGetVillas = await _villaAPIService.GetAllAsync<APIResponse>();
                    string apiGetVillasString = Convert.ToString(apiResponseGetVillas.Result)!;
                    List<VillaDTO> villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(apiGetVillasString)!;

                    vm.VillaNumber = villaNumberUpdateDTO;
                    vm.VillaList = villaList.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
                    return View(vm);
                }
                else
                {
                    return View("Error");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }

           
        }
        [HttpPost]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM vm )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _villaNumberAPIService.UpdateAsync<APIResponse>(vm.VillaNumber);
                    if (response != null && response.isSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        if (response.ErrorMessage.Count > 0)
                        {
                            ModelState.AddModelError("Error", response.ErrorMessage.FirstOrDefault());
                        }
                        // Villa List in vm is not populated
                        // we need to populate it when displaying an error 
                        // so user can re-select

                        var villaResponse = await _villaAPIService.GetAllAsync<APIResponse>();
                        if (villaResponse != null && villaResponse.isSuccess)
                        {
                            string resultString = Convert.ToString(villaResponse.Result);
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

                }
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult>DeleteVillaNumber(int villaNo)
        {
            var apiResponse = await  _villaNumberAPIService.GetAsync<APIResponse>(villaNo);
            if(apiResponse!=null && apiResponse.isSuccess)
            {
                string resultString = Convert.ToString(apiResponse.Result)!;
                var VillaNumberDTO = JsonConvert.DeserializeObject<VillaNumberDTO>(resultString);

                return View(VillaNumberDTO);
            }

            return View("Error");
        }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDTO villaNumber)
        {

            var response = await _villaNumberAPIService.DeleteAsync<APIResponse>(villaNumber.VillaNo.Value);
            if (response != null && response.isSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Error");
        }
    }
}
