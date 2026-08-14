using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AnimalWorld.Web.Models.Zoos
{
    public class ZooSpeciesBindingViewModel
    {
        [Required]
        public int ZooId { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать хотя бы один вид животных")]
        public List<int> SelectedAnimalSpeciesIds { get; set; }

        public List<SelectListItem> Zoos { get; set; }

        public List<SelectListItem> AnimalSpecies { get; set; }
    }
}
