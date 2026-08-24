using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Animals;

namespace AnimalWorld.Web.Mappers.Animals
{
    public class AnimalSpeciesMapper : IMapper<AnimalSpeciesData, AnimalSpeciesViewModel>
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

        public List<AnimalSpeciesViewModel> MapList(List<AnimalSpeciesData> source)
        {
            return source.Select(Map).ToList();
        }
    }
}
