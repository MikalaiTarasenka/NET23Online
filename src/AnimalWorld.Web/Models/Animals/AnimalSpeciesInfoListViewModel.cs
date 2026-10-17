namespace AnimalWorld.Web.Models.Animals
{
    public class AnimalSpeciesInfoListViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

        public List<AnimalSpeciesBriefViewModel> Animals { get; set; }

        public List<int> PageNumbers { get; set; }

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
