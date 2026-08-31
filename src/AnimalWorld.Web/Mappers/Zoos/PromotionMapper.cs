using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Zoos;

namespace AnimalWorld.Web.Mappers.Zoos
{
    public class PromotionMapper : IReverseMapper<PromotionData, PromotionViewModel>
    {
        public PromotionViewModel Map(PromotionData source)
        {
            return new PromotionViewModel
            {
                Id = source.Id,
                ZooId = source.VenueId,
                Name = source.Name,
                Description = source.Description,
                EndDate = source.EndDate,
            };
        }

        public PromotionData ReverseMap(PromotionViewModel destination)
        {
            return new PromotionData
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                EndDate = destination.EndDate,
                VenueId = destination.ZooId
            };
        }
    }
}
