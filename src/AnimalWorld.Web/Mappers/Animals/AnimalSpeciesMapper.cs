using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Animals;

namespace AnimalWorld.Web.Mappers.Animals
{
    public class AnimalSpeciesMapper : IReverseMapper<AnimalSpeciesData, AnimalSpeciesViewModel>
    {
        public AnimalSpeciesViewModel Map(AnimalSpeciesData source)
        {
            return new AnimalSpeciesViewModel
            {
                Name = source.Name,
                Description = source.Description,
                NativeRange = source.NativeRange,
                Url = source.Url,
                Zoos = source.Zoos?.Select(s => s.Name).ToList() ?? new List<string>(),
            };
        }

        public AnimalSpeciesData ReverseMap(AnimalSpeciesViewModel destination)
        {
            return new AnimalSpeciesData
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                NativeRange = destination.NativeRange,
                Url = destination.Url,
                AnimalFamilyId = destination.AnimalFamilyId,
            };
        }
    }
}
