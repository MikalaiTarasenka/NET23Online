namespace AnimalWorld.Web.Helpers
{
    public interface IImageUploadHelper
    {
        Task<string?> SaveAsync(IFormFile? file, string folder, string namePrefix);
    }
}
