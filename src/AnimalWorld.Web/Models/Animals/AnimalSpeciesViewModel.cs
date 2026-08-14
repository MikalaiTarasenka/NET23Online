using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AnimalWorld.Web.Models.Animals
{
    public class AnimalSpeciesViewModel
    {
        [Required]
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        public string Url { get; set; }

        [Required]
        public int AnimalFamilyId { get; set; }

        public List<SelectListItem> AnimalFamilies { get; set; }

        [Required]
        public string NativeRange { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        public List<string> Zoos { get; set; }
    }
}
