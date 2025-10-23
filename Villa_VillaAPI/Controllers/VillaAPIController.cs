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

        public VillaAPIController(IVillaService villaService)
        {
            _villaService = villaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(_villaService.GetVillas());
        }


        [HttpGet("{id}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> getVilla(int id)
        {
            if (id <= 0) return BadRequest();
            try
            {
                var villa = _villaService.GetVilla(id);

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> CreateVilla(VillaDTO dto)
        {
            if (dto == null || dto.Id > 0) return BadRequest();


            //return Ok(_villaService.AddVilla(dto.Name));

            VillaDTO villa = _villaService.AddVilla(dto.Name);

            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [HttpDelete("{id}")]
        public IActionResult DeleteVilla(int id)
        {
            if (id <= 0) return BadRequest("Invalid Villa id");

            bool isDeleted = _villaService.DeleteVilla(id);

            if (!isDeleted) return NotFound();

            return NoContent();
        }


        //[HttpPut("{id}")]
        //public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        //{
        //    if (villaDTO == null ||id!=villaDTO.Id)
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
