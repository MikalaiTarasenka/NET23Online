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

        public ResponseDto Create(PromotionData promotionData)
        {
            if (_promotionRepository.GetByName(promotionData.Name) != null)
            {
                return new ResponseDto
                {
                    Success = false,
                    Error = "Такая акция занята"
                };
            }

            var user = _authService.GetUser();
            promotionData.Creator = user;
            promotionData.CreatorId = user.Id;
            _promotionRepository.Create(promotionData);
            return new ResponseDto { Success = true };
        }

        public void Delete(int id)
        {
            _promotionRepository.Delete(id);
        }

        public PromotionData Get(int id)
        {
            var promotion = _promotionRepository.GetById(id);
            return promotion;
        }

        public List<PromotionData> GetAll()
        {
            var promotions = _promotionRepository.GetAll();
            return promotions;
        }

        public void Update(PromotionData promotionData)
        {
            var promotion = _promotionRepository.GetById(promotionData.Id);
            promotion.Name = promotionData.Name;
            promotion.Description = promotionData.Description;
            promotion.EndDate = promotionData.EndDate;
            promotion.Venue = promotionData.Venue;
            promotion.VenueId = promotionData.VenueId;
            _promotionRepository.Update(promotion);
        }
    }
}
