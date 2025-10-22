using Microsoft.AspNetCore.Mvc;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;
using Villa_VillaAPI.Services;

namespace Villa_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/VillaAPI")]
    public class VillaAPIController:ControllerBase
    {
        private readonly IVillaService _villaService;

        public VillaAPIController(IVillaService villaService)
        {
            _villaService = villaService;
        }

        [HttpGet]
        public IEnumerable<VillaDTO>GetVillas()
        {
            return _villaService.getVillas();
        }
        [HttpGet("{id}")]
        public VillaDTO getVilla(int id)
        {
            return _villaService.getVilla(id);
        }
    }
}
