using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebNet23Online.Controllers.CustomAuthAttribute;
using WebNet23Online.Hubs;
using WebNet23Online.Models.AnimalWorld;
using WebNet23Online.Services.Interfaces;

namespace WebNet23Online.Controllers
{
    public class AnimalWorldController : Controller
    {
        private IAnimalWorldService _animalWorldService;
        private IHubContext<AnimalWorldHub, IAnimalWorldHub> _animalWorldHub;
        private IConfiguration _configuration;

        public AnimalWorldController(IAnimalWorldService animalWorldService, IHubContext<AnimalWorldHub, IAnimalWorldHub> animalWorldHub, IConfiguration configuration)
        {
            _animalWorldService = animalWorldService;
            _animalWorldHub = animalWorldHub;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View(_animalWorldService.GetStartInfo());
        }

        [Authorize]
        [IsModerator]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [IsModerator]
        public IActionResult AddZoo()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [IsModerator]
        public IActionResult AddZoo(ZooViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (_animalWorldService.AddZoo(viewModel))
            {
                return RedirectToAction("Add");
            }
            
            return View();
        }

        [HttpGet]
        [Authorize]
        [IsModerator]
        public IActionResult AddFamily()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [IsModerator]
        public IActionResult AddFamily(AnimalFamilyViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (_animalWorldService.AddAnimalFamily(viewModel))
            {
                return RedirectToAction("Add");
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        [IsModerator]
        public IActionResult AddSpecies()
        {
            return View(_animalWorldService.GetAnimalSpeciesPageInfo());
        }

        [HttpPost]
        [Authorize]
        [IsModerator]
        public IActionResult AddSpecies(AnimalSpeciesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AnimalFamilyNames = _animalWorldService.GetAnimalSpeciesPageInfo().AnimalFamilyNames;
                return View(viewModel);
            }

            if (_animalWorldService.AddAnimalSpecies(viewModel))
            {
                return RedirectToAction("Add");
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        [IsModerator]
        public IActionResult BindZooAndAnimalSpecies()
        {
            return View(_animalWorldService.GetBingZooAndAnimalSpeciesInfo());
        }

        [HttpPost]
        [Authorize]
        [IsModerator]
        public IActionResult BindZooAndAnimalSpecies(BindZooWithAnimalSpeciesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var bindings = _animalWorldService.GetBingZooAndAnimalSpeciesInfo();
                viewModel.Zoos = bindings.Zoos;
                viewModel.AnimalSpecies = bindings.AnimalSpecies;
                return View(viewModel);
            }

            if (_animalWorldService.BindZooWithAnimalSpecies(viewModel.ZooId, viewModel.SelectedAnimalSpeciesIds))
            {
                var zooName = _animalWorldService.GetZooName(viewModel.ZooId);
                var random = new Random();
                var randomId = random.Next(viewModel.SelectedAnimalSpeciesIds.Count);
                var animalSpeciesName = _animalWorldService.GetAnimalSpeciesName(viewModel.SelectedAnimalSpeciesIds[randomId]);
                _animalWorldHub.Clients.All.NewAnimalInZooAppeared(zooName, $"{animalSpeciesName} и другие");
                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize]
        public IActionResult Zoos()
        {
            return View(_animalWorldService.GetAllZoos());
        }

        public IActionResult Promotions()
        {
            return View(_animalWorldService.GetAllPromotions());
        }

        [HttpGet]
        [Authorize]
        [IsModerator]
        public IActionResult AddPromotion()
        {
            var viewModel = _animalWorldService.GetPromotionsPageInfo();
            viewModel.EndDate = DateTime.Now;
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [IsModerator]
        public IActionResult AddPromotion(PromotionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Zoos = _animalWorldService.GetPromotionsPageInfo().Zoos;
                return View(viewModel);
            }

            if (_animalWorldService.AddPromotion(viewModel))
            {
                return RedirectToAction("Add");
            }

            return View();
        }

        public async Task<IActionResult> Gallery()
        {
            var animals = await _animalWorldService.GetRandomAnimalsAsync();
            return View(animals);
        }

        public IActionResult AnimalSpeciesInfo(string? searchCategory = null, string? searchQuery = null)
        {
            return View(_animalWorldService.AnimalSpeciesInfo(searchCategory, searchQuery));
        }

        public IActionResult InterestingFacts()
        {
            InterestingFactsViewModel viewModel = new InterestingFactsViewModel
            {
                FactsApiUrl = _configuration["ApiEndpoints:FactsApi"]
            };
            return View(viewModel);
        }
    }
}
