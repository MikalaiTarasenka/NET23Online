using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Animals
{
    public interface IAnimalFamilyRepository : INamedBaseRepository<AnimalFamilyData>
    {
        List<AnimalFamilyData> GetRandomElements();
    }
}
