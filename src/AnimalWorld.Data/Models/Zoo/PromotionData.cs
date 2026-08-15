using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Models.User;

namespace AnimalWorld.Data.Models.Zoo
{
    public class PromotionData : NamedBaseModel
    {
        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public int VenueId { get; set; }

        public int CreatorId { get; set; }

        public ZooData Venue { get; set; }

        public UserData Creator { get; set; }
    }
}
