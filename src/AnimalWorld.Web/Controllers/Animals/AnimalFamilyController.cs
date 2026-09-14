using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Animals;
using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers.Animals
{
    public class AnimalFamilyController : Controller
    {
        private IAnimalFamilyService _animalFamilyService;
        private IReverseMapper<AnimalFamilyData, AnimalFamilyViewModel> _mapper;

        public AnimalFamilyController(IAnimalFamilyService animalFamilyService, IReverseMapper<AnimalFamilyData, AnimalFamilyViewModel> mapper)
        {
            _animalFamilyService = animalFamilyService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Form(AnimalFamilyViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var animalFamilyData = _mapper.ReverseMap(viewModel);
            if (viewModel.Id == 0)
            {
                var response = await _animalFamilyService.Create(animalFamilyData);
                if (!response.Success)
                {
                    ModelState.AddModelError("Name", response.Error);
                    return View(viewModel);
                }
            }
            else
            {
                await _animalFamilyService.Update(animalFamilyData);
            }

            return RedirectToAction("Moderating", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Form(int id)
        {
            var viewModel = new AnimalFamilyViewModel { Id = id };
            if (id != 0)
            {
                var animalFamilyData = await _animalFamilyService.Get(id);
                viewModel = _mapper.Map(animalFamilyData);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> List()
        {
            var animalfamilies = await _animalFamilyService.GetAll();
            var viewModel = _mapper.MapList(animalfamilies);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _animalFamilyService.Delete(id);
            return RedirectToAction("List", "AnimalFamily");
        }
    }
}
