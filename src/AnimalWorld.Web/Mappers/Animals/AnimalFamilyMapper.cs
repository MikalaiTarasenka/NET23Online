using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Mappers.Interfaces.CustomMappers;
using AnimalWorld.Web.Models.Animals;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Web.Mappers.Animals
{
    public class AnimalFamilyMapper : IAnimalFamilyMapper
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

        public List<SelectListItem> ToSelectListItems(List<AnimalFamilyData> source)
        {
            var animalFamilySelectedList = source.Select(animalFamily => new SelectListItem
            {
                Text = animalFamily.Name,
                Value = animalFamily.Id.ToString()
            });
            return animalFamilySelectedList.ToList();
        }
    }
}
