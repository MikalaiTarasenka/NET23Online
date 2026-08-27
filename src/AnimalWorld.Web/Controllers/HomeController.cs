using AnimalWorld.Core.Apis;
using AnimalWorld.Core.Dtos.Animals;
using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Mappers;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Animals;
using AnimalWorld.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers
{
    public class HomeController : Controller
    {
        private IAnimalFamilyService _animalFamilyService;
        private IAnimalSpeciesService _animalSpeciesService;
        private IAnimalGalleryService _animalGalleryService;
        private IMapper<AnimalFamilyData, AnimalFamilyViewModel> _animalFamilyMapper;
        private IMapper<AnimalSpeciesData, AnimalSpeciesViewModel> _animalSpeciesMapper;
        private IMapper<RandomAnimalDto, RandomAnimalViewModel> _randomAnimalMapper;

        public HomeController(IAnimalFamilyService animalFamilyService, IAnimalSpeciesService animalSpeciesService,
            IMapper<AnimalFamilyData, AnimalFamilyViewModel> animalFamilyMapper, IMapper<AnimalSpeciesData, AnimalSpeciesViewModel> animalSpeciesMapper,
            IAnimalGalleryService animalGalleryService, IMapper<RandomAnimalDto, RandomAnimalViewModel> randomAnimalMapper)
        {
            _animalFamilyService = animalFamilyService;
            _animalSpeciesService = animalSpeciesService;
            _animalFamilyMapper = animalFamilyMapper;
            _animalSpeciesMapper = animalSpeciesMapper;
            _animalGalleryService = animalGalleryService;
            _randomAnimalMapper = randomAnimalMapper;
        }

        public IActionResult Index()
        {
            var animalFamilies = _animalFamilyService.GetRandomAnimals();
            var animalSpecies = _animalSpeciesService.GetRandomAnimals();
            var animalFamilyViewModels = _animalFamilyMapper.MapList(animalFamilies);
            var animalSpeciesViewModels = _animalSpeciesMapper.MapList(animalSpecies);
            var viewModel = new HomePageViewModel
            {
                AnimalFamilies = animalFamilyViewModels,
                AnimalSpecies = animalSpeciesViewModels,
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Gallery()
        {
            var randomAnimalsDto = await _animalGalleryService.GetRandomAnimalsAsync();
            var randomAnimalViewModel = _randomAnimalMapper.MapList(randomAnimalsDto.RandomAnimals);
            var animalGalleryViewModel = new AnimalGalleryViewModel
            {
                RandomAnimals = randomAnimalViewModel
            };
            return View(animalGalleryViewModel);
        }
    }
}
