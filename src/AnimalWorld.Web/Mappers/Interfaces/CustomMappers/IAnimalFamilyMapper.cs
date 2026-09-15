using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Models.Animals;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Web.Mappers.Interfaces.CustomMappers
{
    public interface IAnimalFamilyMapper : IReverseMapper<AnimalFamilyData, AnimalFamilyViewModel>
    {
        public List<SelectListItem> ToSelectListItems(List<AnimalFamilyData> source);
    }
}
