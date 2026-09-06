using AnimalWorld.Data.Dtos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Interfaces.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Zoos
{
    public interface IZooRepository : INamedBaseRepository<ZooData>
    {
        void BindAnimalSpecies(ZooData zooData, List<int> idsToAdd, List<int> idsToRemove);

        List<ZooAnimalFamilyDto> GetAnimalFamiliesByZooIds(List<int> ids);

        List<ZooData> GetZoos(int page, int count);

        int GetZoosCount();

        ZooData GetWithAnimals(int id);
    }
}
