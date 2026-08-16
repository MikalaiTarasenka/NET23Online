using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Models.Users;

namespace AnimalWorld.Data.Models.Zoos
{
    public class ZooData : NamedBaseModel
    {
        public string Address { get; set; }

        public string Description { get; set; }

        public int CreatorId { get; set; }

        public List<AnimalSpeciesData> AnimalSpecies { get; set; }

        public List<TicketData> Tickets { get; set; }

        public List<CommentData> Comments { get; set; }

        public UserData Creator { get; set; }

        public List<PromotionData> Promotions { get; set; }
    }
}
