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
            throw new NotImplementedException();
        }

        public PromotionData Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<PromotionData> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(PromotionData model)
        {
            throw new NotImplementedException();
        }
    }
}
