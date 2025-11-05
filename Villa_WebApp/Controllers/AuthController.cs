using Microsoft.AspNetCore.Mvc;
using Villa_Villa_WebApp.Models.DTO;
using Villa_WebApp.Models;
using Villa_WebApp.Services.IServices;

namespace Villa_WebApp.Controllers
{
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(ILogger<AuthController> logger,IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }


        [HttpGet]
        public async Task<IActionResult>Login()
        {
            LoginRequestDTO loginRequestDTO = new LoginRequestDTO();
            return View(loginRequestDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if(ModelState.IsValid)
            {
                var apiResponse = await _authService.LoginAsync<APIResponse>(loginRequestDTO);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            RegistrationRequestDTO registrationRequestDTO = new RegistrationRequestDTO();
            return View(registrationRequestDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            if (ModelState.IsValid)
            {
                var apiResponse = await _authService.RegisterAsync<APIResponse>(registrationRequestDTO);
                if(apiResponse!=null && apiResponse.isSuccess)
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult>Logout()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }


    }
}
