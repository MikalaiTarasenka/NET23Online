using AnimalWorld.Data.Enums;
using AnimalWorld.Data.Models.Animal;
using AnimalWorld.Data.Models.Zoo;

namespace AnimalWorld.Data.Models.User
{
    public class UserData : BaseModel
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobilephone { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public Language Language { get; set; }
        public virtual List<ZooData> CreatedByMeZoos { get; set; }
        public virtual List<AnimalFamilyData> CreatedByMeAnimalFamilies { get; set; }
        public virtual List<AnimalSpeciesData> CreatedByMeAnimalSpecies { get; set; }
        public virtual List<PromotionData> CreatedByMePromotions { get; set; }
        public virtual List<TicketData> Tickets { get; set; }
        public virtual List<CommentData> Comments { get; set; }
    }
}
