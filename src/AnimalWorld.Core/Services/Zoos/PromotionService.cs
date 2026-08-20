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

        public void Create(PromotionData promotionData)
        {
            if (_promotionRepository.GetByName(promotionData.Name) != null)
            {
                return;
            }

            var user = _authService.GetUser();
            promotionData.Creator = user;
            promotionData.CreatorId = user.Id;
            _promotionRepository.Create(promotionData);
        }
    }
}
