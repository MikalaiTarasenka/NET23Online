using AnimalWorld.Core.Dtos.Users;
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

        public AnimalFamilyData Get(int id)
        {
            var animalFamilyData = _animalFamilyRepository.GetById(id);
            return animalFamilyData;
        }

        public List<AnimalFamilyData> GetAll()
        {
            var animalFamilies = _animalFamilyRepository.GetAll();
            return animalFamilies;
        }

        public ResponseDto Create(AnimalFamilyData animalFamilyData)
        {
            if (_animalFamilyRepository.GetByName(animalFamilyData.Name) != null)
            {
                return new ResponseDto
                {
                    Success = false,
                    Error = "Такой род животных уже знят"
                };
            }

            var user = _authService.GetUser();
            animalFamilyData.Creator = user;
            animalFamilyData.CreatorId = user.Id;
            _animalFamilyRepository.Create(animalFamilyData);
            return new ResponseDto { Success = true };
        }

        public void Update(AnimalFamilyData animalFamilyData)
        {
            var family = _animalFamilyRepository.GetById(animalFamilyData.Id);
            family.Name = animalFamilyData.Name;
            family.Description = animalFamilyData.Description;
            _animalFamilyRepository.Update(family);
        }

        public void Delete(int id)
        {
            _animalFamilyRepository.Delete(id);
        }
    }
}
