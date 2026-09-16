namespace AnimalWorld.Web.Models.Animals
{
    public class AnimalSpeciesInfoViewModel
    {
        public List<AnimalSpeciesBriefViewModel> BriefAnimalSpecies { get; set; }
        public string SearchCategory { get; set; }
        public string SearchQuery { get; set; }
        public string CurrentCategoryText => SearchCategory switch
        {
            "Species" => "Вид",
            "Family" => "Семейство",
            "Range" => "Место обитания",
            _ => "Все поля"
        };
    }
}
