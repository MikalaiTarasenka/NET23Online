using AnimalWorld.Data.Models.Animals;

namespace AnimalWorld.Core.Services.Interfaces.Animals
{
    public interface IAnimalSpeciesService : IBaseService<AnimalSpeciesData>
    {
        List<AnimalSpeciesData> GetRandomAnimals();
    }
}
