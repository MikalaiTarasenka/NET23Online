using AnimalWorld.Data.Dtos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Interfaces.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Zoos
{
    public interface IZooRepository : INamedBaseRepository<ZooData>
    {
        Task BindAnimalSpecies(ZooData zooData, List<int> idsToAdd, List<int> idsToRemove);

        Task<List<ZooAnimalFamilyDto>> GetAnimalFamiliesByZooIds(List<int> ids);

        Task<List<ZooData>> GetZoos(int page, int count);

        Task<int> GetZoosCount();

        Task<ZooData> GetWithAnimals(int id);
    }
}
