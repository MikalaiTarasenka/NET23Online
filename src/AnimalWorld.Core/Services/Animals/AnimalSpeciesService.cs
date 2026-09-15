using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Animals;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Core.Services.Animals
{
    internal class AnimalSpeciesService : IAnimalSpeciesService
    {
        public const string DEFAULT_URL = "/images/animals/default.jpg";
        private IAnimalSpeciesRepository _animalSpeciesRepository;
        private IAnimalFamilyRepository _animalFamilyRepository;
        private IAuthService _authService;

        public AnimalSpeciesService(IAnimalSpeciesRepository animalSpeciesRepository, IAnimalFamilyRepository animalFamilyRepository, IAuthService authService)
        {
            _animalSpeciesRepository = animalSpeciesRepository;
            _animalFamilyRepository = animalFamilyRepository;
            _authService = authService;
        }

        public async Task<List<AnimalSpeciesData>> GetRandomAnimals()
        {
            var animals = await _animalSpeciesRepository.GetRandomElements();
            return animals;
        }

        public async Task<ResponseDto> Create(AnimalSpeciesData animalSpeciesData)
        {
            if (await _animalSpeciesRepository.GetByName(animalSpeciesData.Name) != null)
            {
                return new ResponseDto
                {
                    Success = false,
                    Error = "Такой вид животных занят"
                };
            }

            var user = await _authService.GetUser();
            var animalFamily = await _animalFamilyRepository.GetById(animalSpeciesData.AnimalFamilyId);
            if (animalSpeciesData.Url == null)
            {
                animalSpeciesData.Url = DEFAULT_URL;
            }

            animalSpeciesData.Creator = user;
            animalSpeciesData.CreatorId = user.Id;
            animalSpeciesData.AnimalFamily = animalFamily;
            animalSpeciesData.AnimalFamilyId = animalFamily.Id;
            await _animalSpeciesRepository.Create(animalSpeciesData);
            return new ResponseDto { Success = true };
        }

        public async Task<List<AnimalSpeciesData>> GetAll()
        {
            var animalSpecies = await _animalSpeciesRepository.GetAll();
            return animalSpecies;
        }

        public async Task Update(AnimalSpeciesData modelData)
        {
            var animalSpecies = await _animalSpeciesRepository.GetById(modelData.Id);
            animalSpecies.Name = modelData.Name;
            animalSpecies.Description = modelData.Description;
            animalSpecies.NativeRange = modelData.NativeRange;
            if (!string.IsNullOrEmpty(modelData.Url))
            {
                animalSpecies.Url = modelData.Url;
            }
            
            animalSpecies.AnimalFamily = modelData.AnimalFamily;
            animalSpecies.AnimalFamilyId = modelData.AnimalFamilyId;
            await _animalSpeciesRepository.Update(animalSpecies);
        }

        public async Task Delete(int id)
        {
            await _animalSpeciesRepository.Delete(id);
        }

        public async Task<AnimalSpeciesData> Get(int id)
        {
            var animalSpecies = await _animalSpeciesRepository.GetById(id);
            return animalSpecies;
        }

        public async Task<List<AnimalSpeciesData>> GetWithAnimalFamily(string searchCategory, string searchQuery)
        {
            var animals = await _animalSpeciesRepository.GetAllWithFamily(searchCategory, searchQuery);
            return animals;
        }
    }
}
