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
        public ActionResult<IEnumerable<VillaDTO>>GetVillas()
        {
            return Ok(_villaService.getVillas());
        }
        [HttpGet("{id}")]
        public ActionResult<VillaDTO> getVilla(int id)
        {
            if (id <= 0) return BadRequest();
            try
            {
                var villa = _villaService.getVilla(id);

                if (villa == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(villa);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
