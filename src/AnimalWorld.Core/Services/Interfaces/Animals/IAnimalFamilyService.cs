using AnimalWorld.Data.Models.Animals;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Core.Services.Interfaces.Animals
{
    public interface IAnimalFamilyService : IBaseService<AnimalFamilyData>
    {
        List<AnimalFamilyData> GetRandomAnimals();

        List<SelectListItem> GetSelectListAnimalFamilies();
    }
}
