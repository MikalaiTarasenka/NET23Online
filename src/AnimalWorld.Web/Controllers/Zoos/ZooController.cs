using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Zoos;
using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers.Zoos
{
    public class ZooController : Controller
    {
        private IZooService _zooService;
        private IAnimalSpeciesService _animalSpeciesService;
        private IReverseMapper<ZooData, ZooViewModel> _mapper;

        public ZooController(IZooService zooService, IReverseMapper<ZooData, ZooViewModel> mapper, IAnimalSpeciesService animalSpeciesService)
        {
            _zooService = zooService;
            _mapper = mapper;
            _animalSpeciesService = animalSpeciesService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var zooDatas = await _zooService.GetAll();
            var zooViewModels = _mapper.MapList(zooDatas);
            return View(zooViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Form(ZooViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var zooData = _mapper.ReverseMap(viewModel);
            if (viewModel.Id == 0)
            {
                var response = await _zooService.Create(zooData);
                if (!response.Success)
                {
                    ModelState.AddModelError("Name", response.Error);
                    return View(viewModel);
                }
            }
            else
            {
                await _zooService.Update(zooData);
            }

            return RedirectToAction("Moderating", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Form(int id)
        {
            var viewModel = new ZooViewModel { Id = id };
            if (id != 0)
            {
                var zooData = await _zooService.Get(id);
                viewModel = _mapper.Map(zooData);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> List()
        {
            var zoos = await _zooService.GetAll();
            var viewModel = _mapper.MapList(zoos);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _zooService.Delete(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Binding(int id)
        {
            var zooData = await _zooService.GetWithAnimals(id);
            var zooViewModel = _mapper.Map(zooData);
            var viewModel = new BindingViewModel
            {
                ZooId = id,
                Zoo = zooViewModel,
                AnimalSpecies = await _animalSpeciesService.SelectListAnimalSpecies(),
                SelectedAnimalSpeciesIds = _zooService.ZooAnimalSpeciesIds(zooData.AnimalSpecies)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Binding(BindingViewModel viewModel)
        {
            var zooData = await _zooService.GetWithAnimals(viewModel.ZooId);
            if (!ModelState.IsValid)
            {
                var zooViewModel = _mapper.Map(zooData);
                viewModel = new BindingViewModel
                {
                    Zoo = zooViewModel,
                    AnimalSpecies = await _animalSpeciesService.SelectListAnimalSpecies(),
                    SelectedAnimalSpeciesIds = _zooService.ZooAnimalSpeciesIds(zooData.AnimalSpecies)
                };
                return View(viewModel);
            }

            await _zooService.BindAnimalSpecies(zooData, viewModel.SelectedAnimalSpeciesIds);
            return RedirectToAction("List");
        }
    }
}
