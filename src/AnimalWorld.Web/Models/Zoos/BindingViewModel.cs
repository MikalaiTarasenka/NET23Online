using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AnimalWorld.Web.Models.Zoos
{
    public class BindingViewModel
    {
        public ZooViewModel Zoo { get; set; }

        public List<SelectListItem> AnimalSpecies { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать хотя бы один вид животных")]
        public List<int> SelectedAnimalSpeciesIds { get; set; }
    }
}
