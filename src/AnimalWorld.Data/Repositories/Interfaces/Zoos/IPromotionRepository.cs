using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Interfaces.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Zoos
{
    public interface IPromotionRepository : INamedBaseRepository<PromotionData>
    {
        Task<List<PromotionData>> GetPromotionsIncludeZoo();
    }
}
