using AnimalWorld.Web.Models.Animals;

namespace AnimalWorld.Web.Models.Home
{
    public class HomePageViewModel
    {
        public List<AnimalFamilyViewModel> AnimalFamilies { get; set; }

        public List<AnimalSpeciesViewModel> AnimalSpecies { get; set; }
    }
}
