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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Form(ZooViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var zooData = _mapper.ReverseMap(viewModel);
            if (viewModel.Id == 0)
            {
                var response = _zooService.Create(zooData);
                if (!response.Success)
                {
                    ModelState.AddModelError("Name", response.Error);
                    return View(viewModel);
                }
            }
            else
            {
                _zooService.Update(zooData);
            }

            return RedirectToAction("Moderating", "Home");
        }

        [HttpGet]
        public IActionResult Form(int id)
        {
            var viewModel = new ZooViewModel { Id = id };
            if (id != 0)
            {
                var zooData = _zooService.Get(id);
                viewModel = _mapper.Map(zooData);
            }

            return View(viewModel);
        }

        public IActionResult List()
        {
            var zoos = _zooService.GetAll();
            var viewModel = _mapper.MapList(zoos);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _zooService.Delete(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Binding(int id)
        {
            var zooData = _zooService.GetWithAnimals(id);
            var zooViewModel = _mapper.Map(zooData);
            var selectListAnimalSpecies = _animalSpeciesService.SelectListAnimalSpecies();
            var viewModel = new BindingViewModel
            {
                Zoo = zooViewModel,
                AnimalSpecies = selectListAnimalSpecies,
                SelectedAnimalSpeciesIds = _zooService.ZooAnimalSpeciesIds(zooData.AnimalSpecies)
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Binding(BindingViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
