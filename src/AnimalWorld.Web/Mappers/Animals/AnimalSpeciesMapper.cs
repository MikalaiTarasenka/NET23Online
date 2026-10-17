using AnimalWorld.Core.Dtos;
using AnimalWorld.Core.Dtos.Animals;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Mappers.Interfaces.CustomMappers;
using AnimalWorld.Web.Models.Animals;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Web.Mappers.Animals
{
    public class AnimalSpeciesMapper : IAnimalSpeciesMapper
    {
        public AnimalSpeciesViewModel Map(AnimalSpeciesData source)
        {
            return new AnimalSpeciesViewModel
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                NativeRange = source.NativeRange,
                Url = source.Url,
                AnimalFamilyId = source.AnimalFamilyId,
                Zoos = source.Zoos?.Select(s => s.Name).ToList(),
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

        public AnimalSpeciesInfoListViewModel MapPaginationInfo(PagedResult<AnimalInfoDto> source, string? searchCategory, string? searchQuery)
        {
            return new AnimalSpeciesInfoListViewModel
            {
                Animals = source.Items.Select(x => new AnimalSpeciesBriefViewModel
                {
                    AnimalSpeciesName = x.AnimalSpecies,
                    AnimalFamilyName = x.AnimalFamily,
                    NativeRange = x.Habitat,
                }).ToList(),
                CurrentPage = source.CurrentPage,
                HasNextPage = source.HasNextPage,
                HasPreviousPage = source.HasPreviousPage,
                PageNumbers = source.PageNumbers,
                TotalPages = source.TotalPages,
                SearchQuery = searchQuery,
                SearchCategory = searchCategory
            };
        }

        public List<SelectListItem> ToSelectListItems(List<AnimalSpeciesData> source)
        {
            var selectList = source.Select(animal => new SelectListItem
            {
                Text = animal.Name,
                Value = animal.Id.ToString()
            });
            return selectList.ToList();
        }
    }
}
