using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;
using Villa_VillaAPI.Services.IServices;
using System.Net;

namespace Villa_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IAPIService _APIService;

        public UserController(IUserService userService,ILogger<UserController> logger,IAPIService APIService)
        {
            _userService = userService;
            _logger = logger;
            _APIService = APIService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO loginRequestDTO)
        {
            APIResponse response = new APIResponse();
            try
            {
                var user = await _userService.Login(loginRequestDTO);
                response = _APIService.CreateSuccessResponse(HttpStatusCode.OK, user);
                return Ok(response);
            }
            catch (UnauthorizedAccessException unauthorizedEx)
            {
                response = _APIService.CreateFailureResponse(HttpStatusCode.Unauthorized, new List<string>() { unauthorizedEx.Message });
                return Unauthorized(response);
            }
            catch(Exception ex)
            {
                response = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
                return StatusCode(500, response);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            APIResponse response = new APIResponse();
            try
            {
                var registerResponse = await _userService.Register(registrationRequestDTO);
                if (registerResponse != null)
                {
                    response = _APIService.CreateSuccessResponse(HttpStatusCode.OK, registerResponse);
                    return Ok(response);
                }
                else
                {
                    response = _APIService.CreateFailureResponse(HttpStatusCode.BadRequest,new List<string>());
                    return BadRequest(response);
                }

            }
            catch(InvalidOperationException InvalidOpEx)
            {
                response = _APIService.CreateFailureResponse(HttpStatusCode.Conflict, new List<string>() { InvalidOpEx.Message });
                return Conflict(response);
            }
            catch(Exception ex)
            {
                response = _APIService.CreateFailureResponse(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
                return StatusCode(500, response);
            }
        }

    }
}
