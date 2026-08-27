using AnimalWorld.Core.Dtos.Animals;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Home;

namespace AnimalWorld.Web.Mappers.Animals
{
    public class RandomAnimalMapper : IMapper<RandomAnimalDto, RandomAnimalViewModel>
    {
        public RandomAnimalViewModel Map(RandomAnimalDto source)
        {
            return new RandomAnimalViewModel
            {
                Image = source.Image,
                Fact = source.Fact
            };
        }
    }
}
