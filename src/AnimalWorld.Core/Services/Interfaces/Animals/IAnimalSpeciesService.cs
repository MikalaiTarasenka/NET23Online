using AnimalWorld.Data.Models.Animals;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Core.Services.Interfaces.Animals
{
    public interface IAnimalSpeciesService : IBaseService<AnimalSpeciesData>
    {
        List<AnimalSpeciesData> GetRandomAnimals();

        List<SelectListItem> SelectListAnimalSpecies();
    }
}
