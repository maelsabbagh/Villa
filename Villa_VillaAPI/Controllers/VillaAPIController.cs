using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        private readonly IAPIService _APIService;

        public VillaAPIController(IVillaService villaService,ILogger<VillaAPIController>logger,IAPIService APIService)
        {
            _villaService = villaService;
            _logger = logger;
            _APIService = APIService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                var villas = await _villaService.GetVillas();
                var response = _APIService.CreateSuccessResponse(HttpStatusCode.OK, villas);
                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var response = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError,new List<string>() { ex.Message});
                return StatusCode(500, response);
            }
        }


        [HttpGet("{id}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> getVilla(int id)
        {
            if (id <= 0)
            {
                string errorMessage = $"Invalid id: {id}";
                _logger.LogError(errorMessage);
                var response = _APIService.CreateFailureResponse(HttpStatusCode.BadRequest, new List<string>() { errorMessage });
                return BadRequest(response);

            }
            try
            {
                var villa = await _villaService.GetVilla(id);

                if (villa == null)
                {
                    string errorMessage=$"Villa is not found";
                    _logger.LogError(errorMessage);
                    var response = _APIService.CreateFailureResponse(HttpStatusCode.NotFound, new List<string>() { errorMessage });
                    return NotFound(response);
                }
                else
                {
                    var response = _APIService.CreateSuccessResponse(HttpStatusCode.OK, villa);
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var response = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateVilla(VillaCreateDTO dto)
        {
            if (dto == null)
            {
                var response = _APIService.CreateFailureResponse(HttpStatusCode.BadRequest, new List<string>() { "request body is empty" });
                return BadRequest(response); 
            }

            try
            {

                VillaDTO villa = await _villaService.AddVilla(dto);
                var response = _APIService.CreateSuccessResponse(HttpStatusCode.OK, villa);
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var response = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
                return StatusCode(500, response);
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
                var response = _APIService.CreateFailureResponse(HttpStatusCode.BadRequest, new List<string>() { $"Invalid id: {id}" });
                return BadRequest(response);
            }

            try
            {
                bool isDeleted = await _villaService.DeleteVilla(id);

                if (!isDeleted)
                {
                    var response = _APIService.CreateFailureResponse(HttpStatusCode.NotFound, new List<string>() { "villa not found" });

                    return NotFound(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var response = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
                return StatusCode(500, response);
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
                var response = _APIService.CreateFailureResponse(HttpStatusCode.BadRequest, new List<string>() { "Error" });
                return BadRequest(response);
            }
            try
            {

                bool isUpdated = await _villaService.UpdateVilla(villaDTO);

                if (!isUpdated)
                {
                    var response = _APIService.CreateFailureResponse(HttpStatusCode.NotFound, new List<string>() { "villa not found" });
                    return NotFound(response);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var response = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
                return StatusCode(500, response);
            }
        }

    }
}
