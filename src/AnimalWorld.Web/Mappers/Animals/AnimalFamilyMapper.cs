using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Animals;

namespace AnimalWorld.Web.Mappers.Animals
{
    public class AnimalFamilyMapper : IReverseMapper<AnimalFamilyData, AnimalFamilyViewModel>
    {
        public AnimalFamilyViewModel Map(AnimalFamilyData source)
        {
            return new AnimalFamilyViewModel
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
            };
        }

        public AnimalFamilyData ReverseMap(AnimalFamilyViewModel destination)
        {
            return new AnimalFamilyData
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description
            };
        }
    }
}
