using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Zoos;
using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers.Zoos
{
    public class PromotionController : Controller
    {
        private IPromotionService _promotionService;
        private IZooService _zooService;
        private IReverseMapper<PromotionData, PromotionViewModel> _mapper;

        public PromotionController(IPromotionService promotionService, IReverseMapper<PromotionData, PromotionViewModel> mapper, IZooService zooService)
        {
            _promotionService = promotionService;
            _mapper = mapper;
            _zooService = zooService;
        }

        public IActionResult Index()
        {
            var promotions = _promotionService.GetAll();
            var viewModel = _mapper.MapList(promotions);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Form(PromotionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var promotionData = _mapper.ReverseMap(viewModel);
            if (viewModel.Id == 0)
            {
                var response = _promotionService.Create(promotionData);
                if (!response.Success)
                {
                    ModelState.AddModelError("Name", response.Error);
                    return View(viewModel);
                }
            }
            else
            {
                _promotionService.Update(promotionData);
            }

            return RedirectToAction("Moderating", "Home");
        }

        [HttpGet]
        public IActionResult Form(int id)
        {
            var viewModel = new PromotionViewModel { Id = id };
            if (id != 0)
            {
                var promotionData = _promotionService.Get(id);
                viewModel = _mapper.Map(promotionData);
            }

            viewModel.Zoos = _zooService.GetSelectListsZoo();
            return View(viewModel);
        }

        public IActionResult List()
        {
            var promotions = _promotionService.GetAll();
            var viewModel = _mapper.MapList(promotions);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _promotionService.Delete(id);
            return RedirectToAction("List");
        }
    }
}
