using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Attributes;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Mappers.Interfaces.CustomMappers;
using AnimalWorld.Web.Models.Zoos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers.Zoos
{
    [Authorize]
    public class ZooController : Controller
    {
        private IZooService _zooService;
        private IAnimalSpeciesService _animalSpeciesService;
        private IZooMapper _zooMapper;
        private IAnimalSpeciesMapper _animalSpeciesMapper;

        public ZooController(IZooService zooService, IZooMapper zooMapper, IAnimalSpeciesService animalSpeciesService, IAnimalSpeciesMapper animalSpeciesMapper)
        {
            _zooService = zooService;
            _zooMapper = zooMapper;
            _animalSpeciesService = animalSpeciesService;
            _animalSpeciesMapper = animalSpeciesMapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var zoos = await _zooService.GetPagedZoos(page);
            var zooViewModels = _zooMapper.ToPagedZoos(zoos);
            return View(zooViewModels);
        }

        [HttpPost]
        [AtLeastModerator]
        public async Task<IActionResult> Form(ZooViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var zooData = _zooMapper.ReverseMap(viewModel);
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
        [AtLeastModerator]
        public async Task<IActionResult> Form(int id)
        {
            var viewModel = new ZooViewModel { Id = id };
            if (id != 0)
            {
                var zooData = await _zooService.Get(id);
                viewModel = _zooMapper.Map(zooData);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> List()
        {
            var zoos = await _zooService.GetAll();
            var viewModel = _zooMapper.MapList(zoos);
            return View(viewModel);
        }

        [HttpPost]
        [AtLeastModerator]
        public async Task<IActionResult> Delete(int id)
        {
            await _zooService.Delete(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        [AtLeastModerator]
        public async Task<IActionResult> Binding(int id)
        {
            var zooData = await _zooService.GetWithAnimals(id);
            var zooViewModel = _zooMapper.Map(zooData);
            var animalSpecies = await _animalSpeciesService.GetAll();
            var viewModel = new BindingViewModel
            {
                ZooId = id,
                Zoo = zooViewModel,
                AnimalSpecies = _animalSpeciesMapper.ToSelectListItems(animalSpecies),
                SelectedAnimalSpeciesIds = _zooService.ZooAnimalSpeciesIds(zooData.AnimalSpecies)
            };
            return View(viewModel);
        }

        [HttpPost]
        [AtLeastModerator]
        public async Task<IActionResult> Binding(BindingViewModel viewModel)
        {
            var zooData = await _zooService.GetWithAnimals(viewModel.ZooId);
            if (!ModelState.IsValid)
            {
                var zooViewModel = _zooMapper.Map(zooData);
                var animalSpecies = await _animalSpeciesService.GetAll();
                viewModel = new BindingViewModel
                {
                    Zoo = zooViewModel,
                    AnimalSpecies = _animalSpeciesMapper.ToSelectListItems(animalSpecies),
                    SelectedAnimalSpeciesIds = _zooService.ZooAnimalSpeciesIds(zooData.AnimalSpecies)
                };
                return View(viewModel);
            }

            await _zooService.BindAnimalSpecies(zooData, viewModel.SelectedAnimalSpeciesIds);
            return RedirectToAction("List");
        }
    }
}
