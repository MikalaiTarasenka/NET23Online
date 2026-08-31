using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AnimalWorld.Web.Models.Zoos
{
    public class PromotionViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int ZooId { get; set; }

        public string Place { get; set; }

        public List<SelectListItem>? Zoos { get; set; } = new();

        [Required]
        public DateTime EndDate { get; set; }
    }
}
