using AnimalWorld.Core.Dtos;
using AnimalWorld.Core.Dtos.Animals;
using AnimalWorld.Data.Models.Animals;

namespace AnimalWorld.Core.Services.Interfaces.Animals
{
    public interface IAnimalSpeciesService : IBaseService<AnimalSpeciesData>
    {
        Task<List<AnimalSpeciesData>> GetRandomAnimals();

        Task<PagedResult<AnimalInfoDto>> GetWithAnimalFamily(string searchCategory, string searchQuery, int page);

        Task<List<string>> GetAnimalSpeciesNames();
    }
}
