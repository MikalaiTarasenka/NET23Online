using AnimalWorld.Core.HelperModels;

namespace AnimalWorld.Core.Services.Interfaces.Animals
{
    public interface IAnimalGalleryService
    {
        Task<GalleryHelperModel> GetRandomAnimalsAsync();
    }
}
