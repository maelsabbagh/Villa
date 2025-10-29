using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Villa_VillaAPI.Models.DTO;
using System.Net;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Services.IServices;

namespace Villa_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        private readonly ILogger<VillaNumberAPIController> _logger;
        private readonly IVillaNumberService _villaNumberService;
        private readonly IAPIService _APIService;

        public VillaNumberAPIController(ILogger<VillaNumberAPIController> logger, IVillaNumberService villaNumberService, IAPIService aPIService)
        {
            _logger = logger;
            _villaNumberService = villaNumberService;
            _APIService = aPIService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaNumberDTO>>> GetVillaNumbers()
        {
            try
            {
                var villaNumbers = await _villaNumberService.GetAll();
                var response = _APIService.CreateSuccessResponse(HttpStatusCode.OK, villaNumbers);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                var response = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError, new List<string> { ex.Message });
                return StatusCode(500, response);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        [HttpGet("{villaNo}", Name = "GetVillaNumber")]
        public async Task<ActionResult<VillaNumberDTO>> GetVillaNumber(int villaNo)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                if (villaNo <= 0)
                {
                    apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.BadRequest, new List<string> { $"Invalid villaNo:{villaNo}" });
                    return BadRequest(apiResponse);
                }
                var villaNumberDto = await _villaNumberService.GetVillaNumber(villaNo);

                if (villaNumberDto == null)
                {
                    apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.NotFound, new List<string>());
                    return NotFound(apiResponse);
                }
                var response = _APIService.CreateSuccessResponse(HttpStatusCode.OK, villaNumberDto);

                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError, new List<string> { ex.Message });
                return StatusCode(500, apiResponse);
            }
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber(VillaNumberCreateDTO villaNumberCreateDTO)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                if (villaNumberCreateDTO == null || villaNumberCreateDTO.VillaNo <= 0)
                {
                    apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.BadRequest, new List<string> { $"invalid data" });
                    return BadRequest(apiResponse);
                }

                VillaNumberDTO villaNumberDTO = await _villaNumberService.AddVillaNumber(villaNumberCreateDTO);
                apiResponse = _APIService.CreateSuccessResponse(HttpStatusCode.Created, villaNumberDTO);

                return CreatedAtRoute("GetVillaNumber", new { villaNo = villaNumberDTO.VillaNo }, apiResponse);
            }
            catch (InvalidOperationException invalidOperationEx)
            {
                _logger.LogError(invalidOperationEx, invalidOperationEx.Message);
                apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.Conflict, new List<string> { invalidOperationEx.Message });
                return Conflict(apiResponse);
            }
            catch (KeyNotFoundException keyNotFoundEx)  
            {
                _logger.LogError(keyNotFoundEx, keyNotFoundEx.Message);
                apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.NotFound, new List<string> { keyNotFoundEx.Message });
                return NotFound(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError, new List<string> { ex.Message });
                return StatusCode(500, apiResponse);
            }
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{villaNo}")]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int? villaNo)
        {
            APIResponse apiResponse = new APIResponse();

            try
            {
                if (villaNo == null || villaNo <= 0)
                {
                    apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.BadRequest, new List<string> { $"invalid id" });
                    return BadRequest(apiResponse);
                }

                bool isDeleted = await _villaNumberService.DeleteVillaNumber(villaNo.Value);
                if (!isDeleted)
                {
                    apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.NotFound, new List<string>() { "villa Number not found" });

                    return NotFound(apiResponse);
                }

                return NoContent(); // success 
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError, new List<string> { ex.Message });
                return StatusCode(500, apiResponse);
            }

        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{villaNo}")]
        public async Task<ActionResult<APIResponse>>UpdateVillaNumber(int villaNo, VillaNumberUpdateDTO villaNumberUpdate)
        {
            APIResponse apiResponse = new APIResponse();

            try
            {
                if (villaNumberUpdate == null || villaNumberUpdate.VillaNo <= 0 || villaNo!= villaNumberUpdate.VillaNo)
                {
                    apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.BadRequest, new List<string> { $"invalid data" });
                    return BadRequest(apiResponse);
                }

                bool isUpdated = await _villaNumberService.UpdateVillaNumber(villaNumberUpdate);

                if(!isUpdated)
                {
                    apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.NotFound, new List<string>() { "villa Number not found" });

                    return NotFound(apiResponse);
                }

                return NoContent(); // success
            }
            catch (KeyNotFoundException keyNotFoundEx)
            {
                _logger.LogError(keyNotFoundEx, keyNotFoundEx.Message);
                apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.NotFound, new List<string> { keyNotFoundEx.Message });
                return NotFound(apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                apiResponse = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError, new List<string> { ex.Message });
                return StatusCode(500, apiResponse);
            }
        }
    }
}
