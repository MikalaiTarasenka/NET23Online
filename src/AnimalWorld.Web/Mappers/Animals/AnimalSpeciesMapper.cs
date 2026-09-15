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

        public AnimalSpeciesBriefViewModel MapBrief(AnimalSpeciesData source)
        {
            return new AnimalSpeciesBriefViewModel
            {
                AnimalSpeciesName = source.Name,
                AnimalFamilyName = source.AnimalFamily.Name,
                NativeRange = source.NativeRange,
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
