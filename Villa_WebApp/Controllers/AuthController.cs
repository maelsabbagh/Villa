using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Villa_Utility;
using Villa_Villa_WebApp.Models.DTO;
using Villa_WebApp.Models;
using Villa_WebApp.Services.IServices;
using static Villa_Utility.StaticDetails;
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

                if (apiResponse != null && apiResponse.isSuccess)
                {
                    LoginResponseDTO loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(apiResponse.Result)!)!;

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, loginResponse.User.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Role,loginResponse.User.Role));
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    HttpContext.Session.SetString(StaticDetails.SessionToken, loginResponse.Token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Error", apiResponse!.ErrorMessage!.FirstOrDefault()!);
                    return View(loginRequestDTO);
                }
            }

            ModelState.AddModelError("Error", "Error with data you have entered ");
            return View(loginRequestDTO);
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
                registrationRequestDTO.Role = "user";
                var apiResponse = await _authService.RegisterAsync<APIResponse>(registrationRequestDTO);
                if (apiResponse != null && apiResponse.isSuccess)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ModelState.AddModelError("Error", apiResponse!.ErrorMessage!.FirstOrDefault()!);
                    return View(registrationRequestDTO);
                }
            }

            ModelState.AddModelError("Error", "Error with data you have entered ");
            return View(registrationRequestDTO);
        }

        [HttpGet]
        public async Task<IActionResult>Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(StaticDetails.SessionToken, ""); // empty the session jwt token
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }


    }
}
