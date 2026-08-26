using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IMapper<CredentialsViewModel, CredentialsDto> _authMapper;

        public AuthController(IAuthService authService, IMapper<CredentialsViewModel, CredentialsDto> authMapper)
        {
            _authService = authService;
            _authMapper = authMapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(CredentialsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var credentialsDto = _authMapper.Map(viewModel);
            var loginResult = _authService.Login(credentialsDto);
            if (loginResult.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", loginResult.Error);
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CredentialsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var credentialsDto = _authMapper.Map(viewModel);
            var registerResult = _authService.Register(credentialsDto);
            if (registerResult.Success)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("UserName", registerResult.Error);
                return View(viewModel);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }
    }
}
