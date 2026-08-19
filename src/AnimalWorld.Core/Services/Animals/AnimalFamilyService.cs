using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Animals;

namespace AnimalWorld.Core.Services.Animals
{
    internal class AnimalFamilyService : IAnimalFamilyService
    {
        private IAnimalFamilyRepository _animalFamilyRepository;

        public AnimalFamilyService(IAnimalFamilyRepository animalFamilyRepository)
        {
            _animalFamilyRepository = animalFamilyRepository;
        }

        public List<AnimalFamilyData> GetRandomAnimals()
        {
            var animalFamilies = _animalFamilyRepository.GetRandomElements();
            return animalFamilies;
        }

        public void Create(AnimalFamilyData animalFamilyData)
        {
            _animalFamilyRepository.Create(animalFamilyData);
        }
    }
}
