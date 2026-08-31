using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Zoos;

namespace AnimalWorld.Web.Mappers.Zoos
{
    public class ZooMapper : IReverseMapper<ZooData, ZooViewModel>
    {
        public ZooViewModel Map(ZooData source)
        {
            return new ZooViewModel
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Address = source.Address
            };
        }

        public ZooData ReverseMap(ZooViewModel destination)
        {
            return new ZooData
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                Address = destination.Address
            };
        }
    }
}
