using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Interfaces.Zoos;

namespace AnimalWorld.Core.Services.Zoos
{
    internal class ZooService : IZooService
    {
        private IZooRepository _zooRepository;
        private IAuthService _authService;

        public ZooService(IZooRepository zooRepository, IAuthService authService)
        {
            _zooRepository = zooRepository;
            _authService = authService;
        }

        public async Task<ResponseDto> Create(ZooData zooData)
        {
            if (await _zooRepository.GetByName(zooData.Name) != null)
            {
                return new ResponseDto
                {
                    Success = false,
                    Error = "Такой зоопарк существует"
                };
            }

            var user = await _authService.GetUser();
            zooData.Creator = user;
            zooData.CreatorId = user.Id;
            await _zooRepository.Create(zooData);
            return new ResponseDto { Success = true };
        }

        public async Task Delete(int id)
        {
            await _zooRepository.Delete(id);
        }

        public async Task<ZooData> Get(int id)
        {
            var zoo = await _zooRepository.GetById(id);
            return zoo;
        }

        public async Task<ZooData> GetWithAnimals(int id)
        {
            var zoo = await _zooRepository.GetWithAnimals(id);
            return zoo;
        }

        public List<int> ZooAnimalSpeciesIds(List<AnimalSpeciesData> animalSpecies)
        {
            var animalSpeciesIds = animalSpecies.Select(animal => animal.Id).ToList();
            return animalSpeciesIds;
        }

        public async Task<List<ZooData>> GetAll()
        {
            var zoos = await _zooRepository.GetAll();
            return zoos;
        }

        public async Task BindAnimalSpecies(ZooData zoo, List<int> selectedAnimalSpeciesIds)
        {
            var zooCurentAnimalSpeciesIds = ZooAnimalSpeciesIds(zoo.AnimalSpecies);
            var idsToAdd = selectedAnimalSpeciesIds.Except(zooCurentAnimalSpeciesIds).ToList();
            var IdsToRemove = zooCurentAnimalSpeciesIds.Except(selectedAnimalSpeciesIds).ToList();
            await _zooRepository.BindAnimalSpecies(zoo, idsToAdd, IdsToRemove);
        }

        public async Task Update(ZooData zooData)
        {
            var zoo = await _zooRepository.GetById(zooData.Id);
            zoo.Name = zooData.Name;
            zoo.Description = zooData.Description;
            zoo.Address = zooData.Address;
            await _zooRepository.Update(zoo);
        }
    }
}
