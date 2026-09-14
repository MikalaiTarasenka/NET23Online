using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Core.Services.Interfaces.Animals;
using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Repositories.Interfaces.Animals;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<List<AnimalFamilyData>> GetRandomAnimals()
        {
            var animalFamilies = await _animalFamilyRepository.GetRandomElements();
            return animalFamilies;
        }

        public async Task<AnimalFamilyData> Get(int id)
        {
            var animalFamilyData = await _animalFamilyRepository.GetById(id);
            return animalFamilyData;
        }

        public async Task<List<AnimalFamilyData>> GetAll()
        {
            var animalFamilies = await _animalFamilyRepository.GetAll();
            return animalFamilies;
        }

        public async Task<ResponseDto> Create(AnimalFamilyData animalFamilyData)
        {
            if (await _animalFamilyRepository.GetByName(animalFamilyData.Name) != null)
            {
                return new ResponseDto
                {
                    Success = false,
                    Error = "Такой род животных уже знят"
                };
            }

            var user = await _authService.GetUser();
            animalFamilyData.Creator = user;
            animalFamilyData.CreatorId = user.Id;
            await _animalFamilyRepository.Create(animalFamilyData);
            return new ResponseDto { Success = true };
        }

        public async Task Update(AnimalFamilyData animalFamilyData)
        {
            var family = await _animalFamilyRepository.GetById(animalFamilyData.Id);
            family.Name = animalFamilyData.Name;
            family.Description = animalFamilyData.Description;
            await _animalFamilyRepository.Update(family);
        }

        public async Task Delete(int id)
        {
            await _animalFamilyRepository.Delete(id);
        }

        public async Task<List<SelectListItem>> GetSelectListAnimalFamilies()
        {
            var animalFamlies = await _animalFamilyRepository.GetAll();
            var animalFamilySelectedList = animalFamlies.Select(animalFamily => new SelectListItem
            {
                Text = animalFamily.Name,
                Value = animalFamily.Id.ToString()
            });
            return animalFamilySelectedList.ToList();
        }
    }
}
