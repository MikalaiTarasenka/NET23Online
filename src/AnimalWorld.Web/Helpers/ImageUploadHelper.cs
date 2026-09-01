namespace AnimalWorld.Web.Helpers
{
    internal class ImageUploadHelper : IImageUploadHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageUploadHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string?> SaveAsync(IFormFile? file, string folder, string namePrefix)
        {
            if (file == null)
            {
                return null;
            }

            var pathToWwwRootFolder = _webHostEnvironment.WebRootPath;
            var fileName = $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}-{namePrefix}.jpeg";
            var url = $"/images/animal-species/{fileName}";
            var path = Path.Combine(pathToWwwRootFolder, folder, fileName);
            using (var animalSpeciesImage = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(animalSpeciesImage);
            }

            return url;
        }
    }
}
