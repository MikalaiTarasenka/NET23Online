using AnimalWorld.Data.Models.Animals;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Core.Services.Interfaces.Animals
{
    public interface IAnimalSpeciesService : IBaseService<AnimalSpeciesData>
    {
        Task<List<AnimalSpeciesData>> GetRandomAnimals();

        Task<List<SelectListItem>> SelectListAnimalSpecies();

        Task<List<AnimalSpeciesData>> GetWithAnimalFamily(string searchCategory, string searchQuery);
    }
}
