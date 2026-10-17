using AnimalWorld.Core.Dtos;
using AnimalWorld.Core.Dtos.Animals;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Models.Animals;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Web.Mappers.Interfaces.CustomMappers
{
    public interface IAnimalSpeciesMapper : IReverseMapper<AnimalSpeciesData, AnimalSpeciesViewModel>
    {
        AnimalSpeciesInfoListViewModel MapPaginationInfo(PagedResult<AnimalInfoDto> source, string? searchCategory, string? searchQuery);
        List<SelectListItem> ToSelectListItems(List<AnimalSpeciesData> source);
    }
}
