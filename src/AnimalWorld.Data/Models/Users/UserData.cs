using AnimalWorld.Data.Enums;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Models.Zoos;

namespace AnimalWorld.Data.Models.Users
{
    public class UserData : BaseModel
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public UserRole Role { get; set; }

        public Language Language { get; set; }

        public List<ZooData> CreatedZoos { get; set; }

        public List<AnimalFamilyData> CreatedAnimalFamilies { get; set; }

        public List<AnimalSpeciesData> CreatedAnimalSpecies { get; set; }

        public List<PromotionData> CreatedPromotions { get; set; }

        public List<TicketData> Tickets { get; set; }

        public List<CommentData> Comments { get; set; }
    }
}
