using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Models.Users;

namespace AnimalWorld.Data.Models.Animals
{
    public class AnimalFamilyData : NamedBaseModel
    {
        public string Description { get; set; }

        public int CreatorId { get; set; }

        public List<AnimalSpeciesData> AnimalSpecies { get; set; }

        public UserData Creator { get; set; }
    }
}
