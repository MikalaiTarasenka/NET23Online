using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Interfaces.Zoos;

namespace AnimalWorld.Core.Services.Zoos
{
    internal class PromotionService : IPromotionService
    {
        private IPromotionRepository _promotionRepository;
        private IAuthService _authService;

        public PromotionService(IPromotionRepository promotionRepository, IAuthService authService)
        {
            _promotionRepository = promotionRepository;
            _authService = authService;
        }

        public async Task<ResponseDto> Create(PromotionData promotionData)
        {
            if (await _promotionRepository.GetByName(promotionData.Name) != null)
            {
                return new ResponseDto
                {
                    Success = false,
                    Error = "Такая акция занята"
                };
            }

            var user = await _authService.GetUser();
            promotionData.Creator = user;
            promotionData.CreatorId = user.Id;
            await _promotionRepository.Create(promotionData);
            return new ResponseDto { Success = true };
        }

        public async Task Delete(int id)
        {
            await _promotionRepository.Delete(id);
        }

        public async Task<PromotionData> Get(int id)
        {
            var promotion = await _promotionRepository.GetById(id);
            return promotion;
        }

        public async Task<List<PromotionData>> GetAll()
        {
            var promotions = await _promotionRepository.GetAll();
            return promotions;
        }

        public async Task Update(PromotionData promotionData)
        {
            var promotion = await _promotionRepository.GetById(promotionData.Id);
            promotion.Name = promotionData.Name;
            promotion.Description = promotionData.Description;
            promotion.EndDate = promotionData.EndDate;
            promotion.Venue = promotionData.Venue;
            promotion.VenueId = promotionData.VenueId;
            await _promotionRepository.Update(promotion);
        }
    }
}
