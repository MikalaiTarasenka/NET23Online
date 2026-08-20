using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Core.Dtos.Animals
{
    public class AnimalSpeciesDto
    {
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        public string Url { get; set; }

        public int AnimalFamilyId { get; set; }

        public List<SelectListItem> AnimalFamilies { get; set; }

        public string NativeRange { get; set; }

        public string Description { get; set; }

        public List<string> Zoos { get; set; }
    }
}
