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
        public IActionResult Form(AnimalFamilyViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var animalFamilyData = _mapper.ReverseMap(viewModel);
            if (viewModel.Id == 0)
            {
                var response = _animalFamilyService.Create(animalFamilyData);
                if (!response.Success)
                {
                    ModelState.AddModelError("Name", response.Error);
                    return View(viewModel);
                }
            }
            else
            {
                _animalFamilyService.Update(animalFamilyData);
            }

            return RedirectToAction("Moderating", "Home");
        }

        [HttpGet]
        public IActionResult Form(int id)
        {
            var viewModel = new AnimalFamilyViewModel { Id = id };
            if (id != 0)
            {
                var animalFamilyData = _animalFamilyService.Get(id);
                viewModel = _mapper.Map(animalFamilyData);
            }

            return View(viewModel);
        }

        public IActionResult List()
        {
            var animalfamilies = _animalFamilyService.GetAll();
            var viewModel = _mapper.MapList(animalfamilies);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _animalFamilyService.Delete(id);
            return RedirectToAction("List", "AnimalFamily");
        }
    }
}
