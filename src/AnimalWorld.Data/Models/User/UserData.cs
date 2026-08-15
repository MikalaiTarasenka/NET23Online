using AnimalWorld.Data.Enums;
using AnimalWorld.Data.Models.Animal;
using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Models.Zoo;

namespace AnimalWorld.Data.Models.User
{
    public class UserData : BaseModel
    {
        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mobilephone { get; set; }

        public string PasswordHash { get; set; }

        public UserRole Role { get; set; }

        public Language Language { get; set; }

        public List<ZooData> CreatedByMeZoos { get; set; }

        public List<AnimalFamilyData> CreatedByMeAnimalFamilies { get; set; }

        public List<AnimalSpeciesData> CreatedByMeAnimalSpecies { get; set; }

        public List<PromotionData> CreatedByMePromotions { get; set; }

        public List<TicketData> Tickets { get; set; }

        public List<CommentData> Comments { get; set; }
    }
}
