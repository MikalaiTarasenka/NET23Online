using AnimalWorld.Data.Models.Animals;

namespace AnimalWorld.Core.Services.Interfaces.Animals
{
    public interface IAnimalFamilyService : IBaseService<AnimalFamilyData>
    {
        List<AnimalFamilyData> GetRandomAnimals();
    }
}
