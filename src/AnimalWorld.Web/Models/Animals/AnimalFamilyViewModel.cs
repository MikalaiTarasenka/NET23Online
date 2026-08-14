using System.ComponentModel.DataAnnotations;

namespace AnimalWorld.Web.Models.Animals
{
    public class AnimalFamilyViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
    }
}
