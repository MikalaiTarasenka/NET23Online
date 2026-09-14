using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Common;
using AnimalWorld.Data.Repositories.Interfaces.Zoos;
using Microsoft.EntityFrameworkCore;

namespace AnimalWorld.Data.Repositories.Zoos
{
    internal class PromotionRepository : NamedBaseRepository<PromotionData>, IPromotionRepository
    {
        public PromotionRepository(WebContext context) : base(context) { }

        public async Task<List<PromotionData>> GetPromotionsIncludeZoo()
        {
            return await _dbSet
                .Include(p => p.Venue)
                .ToListAsync();
        }
    }
}
