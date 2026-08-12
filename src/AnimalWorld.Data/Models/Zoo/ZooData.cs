using AnimalWorld.Data.Models.Animal;
using AnimalWorld.Data.Models.User;

namespace AnimalWorld.Data.Models.Zoo
{
    public class ZooData : BaseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public virtual List<AnimalSpeciesData> AnimalSpecies { get; set; }
        public virtual List<TicketData> Tickets { get; set; }
        public virtual List<CommentData> Comments { get; set; }
        public virtual UserData Creator { get; set; }
        public virtual List<PromotionData> Promotions { get; set; }
    }
}
