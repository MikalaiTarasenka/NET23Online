using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Animals;

namespace AnimalWorld.Core.Services.Animals
{
    internal class AnimalSpeciesService : IAnimalSpeciesService
    {
        public const string DEFAULT_URL = "/images/animal-world/default.jpg";
        private IAnimalSpeciesRepository _animalSpeciesRepository;
        private IAnimalFamilyRepository _animalFamilyRepository;
        private IAuthService _authService;

        public AnimalSpeciesService(IAnimalSpeciesRepository animalSpeciesRepository, IAnimalFamilyRepository animalFamilyRepository, IAuthService authService)
        {
            _animalSpeciesRepository = animalSpeciesRepository;
            _animalFamilyRepository = animalFamilyRepository;
            _authService = authService;
        }

        public List<AnimalSpeciesData> GetRandomAnimals()
        {
            var animals = _animalSpeciesRepository.GetRandomElements();
            return animals;
        }

        public ResponseDto Create(AnimalSpeciesData animalSpeciesData)
        {
            if (_animalSpeciesRepository.GetByName(animalSpeciesData.Name) != null)
            {
                return new ResponseDto
                {
                    Success = false,
                    Error = "Такой вид животных занят"
                };
            }

            var user = _authService.GetUser();
            var animalFamily = _animalFamilyRepository.GetById(animalSpeciesData.AnimalFamilyId);
            animalSpeciesData.Creator = user;
            animalSpeciesData.CreatorId = user.Id;
            animalSpeciesData.AnimalFamily = animalFamily;
            animalSpeciesData.AnimalFamilyId = animalFamily.Id;
            _animalSpeciesRepository.Create(animalSpeciesData);
            return new ResponseDto { Success = true };
        }

        public List<AnimalSpeciesData> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(AnimalSpeciesData model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public AnimalSpeciesData Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
