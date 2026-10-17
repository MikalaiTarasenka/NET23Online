using AnimalWorld.Core.Dtos;
using AnimalWorld.Core.Dtos.Animals;
using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Animals;

namespace AnimalWorld.Core.Services.Animals
{
    internal class AnimalSpeciesService : IAnimalSpeciesService
    {
        public const string DEFAULT_URL = "/images/animals/default.jpg";
        private IAnimalSpeciesRepository _animalSpeciesRepository;
        private IAnimalFamilyRepository _animalFamilyRepository;
        private IAuthService _authService;
        public const int COUNT_ANIMALS_PER_PAGE = 10;

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

        public async Task<PagedResult<AnimalInfoDto>> GetWithAnimalFamily(string searchCategory, string searchQuery, int page)
        {
            var count = await _animalSpeciesRepository.Count(searchCategory, searchQuery);
            if (count == 0)
            {
                return new PagedResult<AnimalInfoDto> { Items = new List<AnimalInfoDto>() };
            }

            var totalPages = (count + COUNT_ANIMALS_PER_PAGE - 1) / COUNT_ANIMALS_PER_PAGE;
            if (page < 1)
            {
                page = 1;
            }

            if (page > totalPages)
            {
                page = totalPages;
            }

            var animals = await _animalSpeciesRepository.GetAllWithFamily(searchCategory, searchQuery, page, COUNT_ANIMALS_PER_PAGE);
            var animalInfo = animals.Select(x => new AnimalInfoDto
            {
                Id = x.Id,
                AnimalSpecies = x.Name,
                AnimalFamily = x.AnimalFamily.Name,
                Habitat = x.NativeRange
            }).ToList();
            return new PagedResult<AnimalInfoDto>
            {
                Items = animalInfo,
                CurrentPage = page,
                HasNextPage = page < totalPages,
                HasPreviousPage = page > 1,
                PageNumbers = Enumerable.Range(1, totalPages).ToList(),
                TotalPages = totalPages,
            };
        }

        public async Task<List<string>> GetAnimalSpeciesNames()
        {
            var animals = await _animalSpeciesRepository.GetAllAnimalSpeciesNames();
            return animals;
        }
    }
}
