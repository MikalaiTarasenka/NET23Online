using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Data.Enums;
using AnimalWorld.Data.Models.Users;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Web.Controllers
{
    public class UsersController : Controller
    {
        private IUserProfileService _userProfileService;
        private IReverseMapper<UserData, UserProfileViewModel> _userMapper;

        public UsersController(IUserProfileService userProfileService, IReverseMapper<UserData, UserProfileViewModel> userMapper)
        {
            _userProfileService = userProfileService;
            _userMapper = userMapper;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var userData = _userProfileService.Get();
            var userProfileViewModel = _userMapper.Map(userData);
            userProfileViewModel.Languages = Enum
                .GetNames<Language>()
                .Select(x => new SelectListItem
                {
                    Text = x,
                    Value = x,
                    Selected = x == userProfileViewModel.Language.ToString()
                })
                .ToList();
            return View(userProfileViewModel);
        }

        [HttpPost]
        public IActionResult Profile(UserProfileViewModel viewModel)
        {
            var userData = _userMapper.ReverseMap(viewModel);
            _userProfileService.Update(userData);
            return RedirectToAction("Profile");
        }
    }
}
