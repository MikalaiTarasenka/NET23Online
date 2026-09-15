using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Attributes;
using AnimalWorld.Web.Helpers;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Mappers.Interfaces.CustomMappers;
using AnimalWorld.Web.Models.Animals;
using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers.Animals
{
    public class AnimalSpeciesController : Controller
    {
        private IAnimalSpeciesService _animalSpeciesService;
        private IAnimalFamilyService _animalFamilyService;
        private IAuthService _authService;
        private IAnimalSpeciesMapper _speciesMapper;
        private IAnimalFamilyMapper _familyMapper;
        private IImageUploadHelper _imageUploadHelper;

        public AnimalSpeciesController(IAnimalSpeciesService animalSpeciesService, IAnimalSpeciesMapper speciesMapper,
            IAnimalFamilyService animalFamilyService, IImageUploadHelper imageUploadHelper, IAuthService authService, IAnimalFamilyMapper familyMapper)
        {
            _animalSpeciesService = animalSpeciesService;
            _speciesMapper = speciesMapper;
            _animalFamilyService = animalFamilyService;
            _imageUploadHelper = imageUploadHelper;
            _authService = authService;
            _familyMapper = familyMapper;
        }

        [HttpPost]
        [AtLeastModerator]
        public async Task<IActionResult> Form(AnimalSpeciesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var animalFamilies = await _animalFamilyService.GetAll();
                viewModel.AnimalFamilies = _familyMapper.ToSelectListItems(animalFamilies);
                return View(viewModel);
            }

            var userName = _authService.GetUserName();
            if (viewModel.Image != null)
            {
                var url = await _imageUploadHelper.SaveAsync(viewModel.Image, "images\\animals", userName);
                viewModel.Url = url;
            }

            var animalSpeciesData = _speciesMapper.ReverseMap(viewModel);
            if (viewModel.Id == 0)
            {
                var response = await _animalSpeciesService.Create(animalSpeciesData);
                if (!response.Success)
                {
                    ModelState.AddModelError("Name", response.Error);
                    return View(viewModel);
                }
            }
            else
            {
                await _animalSpeciesService.Update(animalSpeciesData);
            }

            return RedirectToAction("Moderating", "Home");
        }

        [HttpGet]
        [AtLeastModerator]
        public async Task<IActionResult> Form(int id)
        {
            var viewModel = new AnimalSpeciesViewModel { Id = id };
            if (id != 0)
            {
                var animalSpeciesData = await _animalSpeciesService.Get(id);
                viewModel = _speciesMapper.Map(animalSpeciesData);
            }

            var animalFamilies = await _animalFamilyService.GetAll();
            viewModel.AnimalFamilies = _familyMapper.ToSelectListItems(animalFamilies);
            return View(viewModel);
        }

        [AtLeastModerator]
        public async Task<IActionResult> List()
        {
            var animalSpecies = await _animalSpeciesService.GetAll();
            var viewModel = _speciesMapper.MapList(animalSpecies);
            return View(viewModel);
        }

        [HttpPost]
        [AtLeastModerator]
        public async Task<IActionResult> Delete(int id)
        {
            await _animalSpeciesService.Delete(id);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Info(string? searchCategory = null, string? searchQuery = null)
        {
            var animals = await _animalSpeciesService.GetWithAnimalFamily(searchCategory, searchQuery);
            var briefViewModel = _speciesMapper.MapBriefList(animals);
            var viewModel = new AnimalSpeciesInfoViewModel
            {
                BriefAnimalSpecies = briefViewModel,
                SearchCategory = searchCategory,
                SearchQuery = searchQuery
            };
            return View(viewModel);
        }
    }
}
