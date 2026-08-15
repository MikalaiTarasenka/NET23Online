using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Models.User;

namespace AnimalWorld.Data.Models.Animal
{
    public class AnimalFamilyData : NamedBaseModel
    {
        public string Description { get; set; }

        public int CreatorId { get; set; }

        public List<AnimalSpeciesData> Species { get; set; }

        public UserData Creator { get; set; }
    }
}
