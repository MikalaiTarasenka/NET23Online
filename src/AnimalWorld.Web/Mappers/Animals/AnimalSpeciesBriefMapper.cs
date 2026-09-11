using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Animals;

namespace AnimalWorld.Web.Mappers.Animals
{
    public class AnimalSpeciesBriefMapper : IMapper<AnimalSpeciesData, AnimalSpeciesBriefViewModel>
    {
        public AnimalSpeciesBriefViewModel Map(AnimalSpeciesData source)
        {
            return new AnimalSpeciesBriefViewModel
            {
                AnimalSpeciesName = source.Name,
                AnimalFamilyName = source.AnimalFamily.Name,
                NativeRange = source.NativeRange,
            };
        }
    }
}
