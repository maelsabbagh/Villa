using Microsoft.AspNetCore.Mvc;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;
using Villa_VillaAPI.Services;

namespace Villa_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/VillaAPI")]
    public class VillaAPIController : ControllerBase
    {
        private readonly IVillaService _villaService;
        private readonly ILogger<VillaAPIController> _logger;

        public VillaAPIController(IVillaService villaService,ILogger<VillaAPIController>logger)
        {
            _villaService = villaService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            try
            {
                return Ok(await _villaService.GetVillas());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpGet("{id}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> getVilla(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Invalid id: {id}");
                return BadRequest();
            }
            try
            {
                var villa = await _villaService.GetVilla(id);

                if (villa == null)
                {
                    _logger.LogError($"Villa is not found");
                    return NotFound();
                }
                else
                {
                    return Ok(villa);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDTO>> CreateVilla(VillaCreateDTO dto)
        {
            if (dto == null) return BadRequest();

            try
            {

                VillaDTO villa = await _villaService.AddVilla(dto);

                return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Invalid id: {id}");
                return BadRequest("Invalid Villa id");
            }

            try
            {
                bool isDeleted = await _villaService.DeleteVilla(id);

                if (!isDeleted) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            try
            {

                bool isUpdated = await _villaService.UpdateVilla(villaDTO);

                if (!isUpdated) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
