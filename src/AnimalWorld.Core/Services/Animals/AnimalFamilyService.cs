using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Animals;

namespace AnimalWorld.Core.Services.Animals
{
    internal class AnimalFamilyService : IAnimalFamilyService
    {
        private IAnimalFamilyRepository _animalFamilyRepository;
        private IAuthService _authService;

        public AnimalFamilyService(IAnimalFamilyRepository animalFamilyRepository, IAuthService authService)
        {
            _animalFamilyRepository = animalFamilyRepository;
            _authService = authService;
        }

        public List<AnimalFamilyData> GetRandomAnimals()
        {
            var animalFamilies = _animalFamilyRepository.GetRandomElements();
            return animalFamilies;
        }

        public void Create(AnimalFamilyData animalFamilyData)
        {
            if (_animalFamilyRepository.GetByName(animalFamilyData.Name) != null)
            {
               return;
            }

            var user = _authService.GetUser();
            animalFamilyData.Creator = user;
            animalFamilyData.CreatorId = user.Id;
            _animalFamilyRepository.Create(animalFamilyData);
        }
    }
}
