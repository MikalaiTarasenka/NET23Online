using AnimalWorld.Data.Models.User;

namespace AnimalWorld.Data.Models.Zoo
{
    public class PromotionData : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public int VenueId { get; set; }
        public int CreatorId { get; set; }
        public virtual ZooData Venue { get; set; }
        public virtual UserData Creator { get; set; }
    }
}
