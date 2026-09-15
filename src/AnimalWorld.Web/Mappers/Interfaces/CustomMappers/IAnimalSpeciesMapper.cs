using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Models.Animals;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Web.Mappers.Interfaces.CustomMappers
{
    public interface IAnimalSpeciesMapper : IReverseMapper<AnimalSpeciesData, AnimalSpeciesViewModel>
    {
        AnimalSpeciesBriefViewModel MapBrief(AnimalSpeciesData source);
        List<AnimalSpeciesBriefViewModel> MapBriefList(List<AnimalSpeciesData> source)
        {
            return source
                .Select(MapBrief)
                .ToList();
        }
        List<SelectListItem> ToSelectListItems(List<AnimalSpeciesData> source);
    }
}
