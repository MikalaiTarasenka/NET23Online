using AnimalWorld.Core.Apis;
using AnimalWorld.Core.HelperModels;
using AnimalWorld.Core.Services.Interfaces.Animals;

namespace AnimalWorld.Core.Services.Animals
{
    internal class AnimalGalleryService : IAnimalGalleryService
    {
        private const int RANDOM_ANIMAL_IMAGE_COUNT = 9;
        private RandomAnimalApi _randomAnimalApi;
        private Random _random;

        public AnimalGalleryService(RandomAnimalApi randomAnimalApi)
        {
            _randomAnimalApi = randomAnimalApi;
            _random = new Random();
        }

        public async Task<GalleryHelperModel> GetRandomAnimalsAsync()
        {
            var animalImages = await GetRandomAnimalImages();
            var gallery = new GalleryHelperModel
            {
                RandomAnimals = animalImages,
            };
            return gallery;
        }

        private async Task<List<RandomAnimalHelperModel>> GetRandomAnimalImages()
        {
            var animalSpecies = await _randomAnimalApi.GetAnimalSpecies();
            var tasks = new List<Task<RandomAnimalHelperModel>>();
            for (var i = 0; i < RANDOM_ANIMAL_IMAGE_COUNT; i++)
            {
                var index = _random.Next(animalSpecies.Count);
                var selectedType = animalSpecies[index];
                tasks.Add(_randomAnimalApi.GetRandomAnimal(selectedType));
            }

            var animalWorldRandomAnimals = await Task.WhenAll(tasks);
            return animalWorldRandomAnimals.ToList();
        }
    }
}
