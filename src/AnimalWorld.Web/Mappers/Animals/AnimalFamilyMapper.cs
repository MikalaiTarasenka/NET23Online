using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Animals;

namespace AnimalWorld.Web.Mappers.Animals
{
    public class AnimalFamilyMapper : IMapper<AnimalFamilyData, AnimalFamilyViewModel>
    {
        public AnimalFamilyViewModel Map(AnimalFamilyData source)
        {
            return new AnimalFamilyViewModel
            {
                Name = source.Name,
                Description = source.Description,
            };
        }
    }
}
