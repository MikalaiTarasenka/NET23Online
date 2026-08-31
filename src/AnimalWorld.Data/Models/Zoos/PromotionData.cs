using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalWorld.Data.Models.Zoos
{
    public class PromotionData : NamedBaseModel
    {
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public int VenueId { get; set; }

        public int CreatorId { get; set; }

        public ZooData Venue { get; set; }

        public UserData Creator { get; set; }
    }
}
