namespace WebNet23Online.Models.AnimalWorld
{
    public class PagedZooListViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public List<ZooViewModel> Zoos { get; set; }
        public List<int> PageNumbers { get; set; }
    }
}
