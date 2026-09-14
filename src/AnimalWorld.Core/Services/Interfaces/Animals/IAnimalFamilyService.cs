using AnimalWorld.Data.Models.Animals;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Core.Services.Interfaces.Animals
{
    public interface IAnimalFamilyService : IBaseService<AnimalFamilyData>
    {
        Task<List<AnimalFamilyData>> GetRandomAnimals();

        Task<List<SelectListItem>> GetSelectListAnimalFamilies();
    }
}
