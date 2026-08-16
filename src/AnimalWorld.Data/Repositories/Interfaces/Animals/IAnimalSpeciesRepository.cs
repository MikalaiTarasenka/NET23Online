using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Animals
{
    public interface IAnimalSpeciesRepository : INamedBaseRepository<AnimalSpeciesData>
    {
        List<AnimalSpeciesData> GetRandomElements();

        List<string> GetAllAnimalSpeciesNames();

        List<AnimalSpeciesData> GetAllWithFamily(string searchCategory, string searchQuery);
    }
}
