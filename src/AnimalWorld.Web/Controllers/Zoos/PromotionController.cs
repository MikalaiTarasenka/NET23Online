using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Attributes;
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

        public async Task<IActionResult> Index()
        {
            var promotions = await _promotionService.GetAll();
            var viewModel = _mapper.MapList(promotions);
            return View(viewModel);
        }

        [HttpPost]
        [AtLeastModerator]
        public async Task<IActionResult> Form(PromotionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Zoos = await _zooService.GetSelectListsZoo();
                return View(viewModel);
            }

            var promotionData = _mapper.ReverseMap(viewModel);
            if (viewModel.Id == 0)
            {
                var response = await _promotionService.Create(promotionData);
                if (!response.Success)
                {
                    ModelState.AddModelError("Name", response.Error);
                    return View(viewModel);
                }
            }
            else
            {
                await _promotionService.Update(promotionData);
            }

            return RedirectToAction("Moderating", "Home");
        }

        [HttpGet]
        [AtLeastModerator]
        public async Task<IActionResult> Form(int id)
        {
            var viewModel = new PromotionViewModel { Id = id };
            if (id != 0)
            {
                var promotionData = await _promotionService.Get(id);
                viewModel = _mapper.Map(promotionData);
            }

            viewModel.Zoos = await _zooService.GetSelectListsZoo();
            return View(viewModel);
        }

        [AtLeastModerator]
        public async Task<IActionResult> List()
        {
            var promotions = await _promotionService.GetAll();
            var viewModel = _mapper.MapList(promotions);
            return View(viewModel);
        }

        [HttpPost]
        [AtLeastModerator]
        public async Task<IActionResult> Delete(int id)
        {
            await _promotionService.Delete(id);
            return RedirectToAction("List");
        }
    }
}
