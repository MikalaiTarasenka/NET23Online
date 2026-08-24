using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Web.Mappers.Interfaces.Users;
using AnimalWorld.Web.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers
{
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IAuthMapper _authMapper;

        public AuthController(IAuthService authService, IAuthMapper authMapper)
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
            _authService.Login(credentialsDto);
            return RedirectToAction("Index", "Home");
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
            _authService.Register(credentialsDto);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }
    }
}
