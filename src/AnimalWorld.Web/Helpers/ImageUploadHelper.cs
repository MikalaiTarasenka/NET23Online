namespace AnimalWorld.Web.Helpers
{
    internal class ImageUploadHelper : IImageUploadHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageUploadHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveAsync(IFormFile file, string folder, string namePrefix)
        {
            if (!await IsImage(file))
            {
                return null;
            }

            var pathToWwwRootFolder = _webHostEnvironment.WebRootPath;
            var fileName = $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss}-{Guid.NewGuid()}-{namePrefix}.jpeg";
            var url = $"/{folder}/{fileName}";
            var path = Path.Combine(pathToWwwRootFolder, folder, fileName);
            using (var animalSpeciesImage = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(animalSpeciesImage);
            }

            return url;
        }

        private async Task<bool> IsImage(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var header = new byte[4];
                await stream.ReadAsync(header, 0, 4);
                bool isImage = (header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF) || (header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47);
                return isImage;
            }
        }
    }
}
