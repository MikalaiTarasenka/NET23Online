using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AnimalWorld.Web.Models.Animals
{
    public class AnimalSpeciesViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IFormFile? Image { get; set; }

        public string? Url { get; set; }

        [Required]
        public int AnimalFamilyId { get; set; }

        public List<SelectListItem>? AnimalFamilies { get; set; }

        [Required]
        public string NativeRange { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        private const int SHORT_DESCRIPTION_LENGTH = 150;

        public string ShortDescription
        {
            get
            {
                return Description.Length <= SHORT_DESCRIPTION_LENGTH
                    ? Description
                    : Description.Substring(0, SHORT_DESCRIPTION_LENGTH) + "...";
            }
        }

        public List<string> Zoos { get; set; }
    }
}
