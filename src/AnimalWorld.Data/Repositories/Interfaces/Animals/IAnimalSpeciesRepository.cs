using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Animals
{
    public interface IAnimalSpeciesRepository : INamedBaseRepository<AnimalSpeciesData>
    {
        Task<List<AnimalSpeciesData>> GetRandomElements();

        Task<List<string>> GetAllAnimalSpeciesNames();

        Task<List<AnimalSpeciesData>> GetAllWithFamily(string searchCategory, string searchQuery);
    }
}
