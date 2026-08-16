using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalWorld.Data.Repositories.Interfaces.Zoos
{
    public interface IPromotionRepository : INamedBaseRepository<PromotionData>
    {
        List<PromotionData> GetPromotionsIncludeZoo();
    }
}
