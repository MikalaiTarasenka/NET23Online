using AnimalWorld.Data.Models.Animals;

namespace AnimalWorld.Core.Services.Interfaces.Animals
{
    public interface IAnimalSpeciesService : IBaseService<AnimalSpeciesData>
    {
        Task<List<AnimalSpeciesData>> GetRandomAnimals();

        Task<List<AnimalSpeciesData>> GetWithAnimalFamily(string searchCategory, string searchQuery);
    }
}
