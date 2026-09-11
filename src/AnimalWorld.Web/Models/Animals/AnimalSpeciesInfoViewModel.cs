namespace AnimalWorld.Web.Models.Animals
{
    public class AnimalSpeciesInfoViewModel
    {
        public List<AnimalSpeciesBriefViewModel> BriefAnimalSpecies { get; set; }
        public string SearchCategory { get; set; }
        public string SearchQuery { get; set; }
    }
}
