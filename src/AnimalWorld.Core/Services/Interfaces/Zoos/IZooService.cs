using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Models.Zoos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Core.Services.Interfaces.Zoos
{
    public interface IZooService : IBaseService<ZooData>
    {
        List<SelectListItem> GetSelectListsZoo();

        ZooData GetWithAnimals(int id);

        List<int> ZooAnimalSpeciesIds(List<AnimalSpeciesData> animalSpecies);
    }
}
