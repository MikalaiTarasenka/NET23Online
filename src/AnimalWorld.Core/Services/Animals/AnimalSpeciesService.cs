using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Animals;

namespace AnimalWorld.Core.Services.Animals
{
    internal class AnimalSpeciesService : IAnimalSpeciesService
    {
        public const string DEFAULT_URL = "/images/animal-species/default.jpg";
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
            if (animalSpeciesData.Url == null)
            {
                animalSpeciesData.Url = DEFAULT_URL;
            }

            animalSpeciesData.Creator = user;
            animalSpeciesData.CreatorId = user.Id;
            animalSpeciesData.AnimalFamily = animalFamily;
            animalSpeciesData.AnimalFamilyId = animalFamily.Id;
            _animalSpeciesRepository.Create(animalSpeciesData);
            return new ResponseDto { Success = true };
        }

        public List<AnimalSpeciesData> GetAll()
        {
            var animalSpecies = _animalSpeciesRepository.GetAll();
            return animalSpecies;
        }

        public void Update(AnimalSpeciesData modelData)
        {
            var animalSpecies = _animalSpeciesRepository.GetById(modelData.Id);
            animalSpecies.Name = modelData.Name;
            animalSpecies.Description = modelData.Description;
            animalSpecies.NativeRange = modelData.NativeRange;
            animalSpecies.Url = modelData.Url;
            animalSpecies.AnimalFamily = modelData.AnimalFamily;
            animalSpecies.AnimalFamilyId = modelData.AnimalFamilyId;
            _animalSpeciesRepository.Update(animalSpecies);
        }

        public void Delete(int id)
        {
            _animalSpeciesRepository.Delete(id);
        }

        public AnimalSpeciesData Get(int id)
        {
            var animalSpecies = _animalSpeciesRepository.GetById(id);
            return animalSpecies;
        }
    }
}
