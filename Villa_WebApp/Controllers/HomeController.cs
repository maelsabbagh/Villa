using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_WebApp.Models;
using Villa_WebApp.Models.DTO;
using Villa_WebApp.Services.IServices;

namespace Villa_WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IVillaAPIService _villaAPIService;
    private readonly IMapper _mapper;
    public HomeController(ILogger<HomeController> logger,IVillaAPIService villaAPIService,IMapper mapper)
    {
        _logger = logger;
        _villaAPIService = villaAPIService;
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

    public IActionResult Privacy()
    {
        return View();
    }

   
}
