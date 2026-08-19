using AnimalWorld.Core.Dtos.Animals;

namespace AnimalWorld.Core.Services.Interfaces.Animals
{
    public interface IAnimalGalleryService
    {
        Task<AnimalGalleryDto> GetRandomAnimalsAsync();
    }
}
