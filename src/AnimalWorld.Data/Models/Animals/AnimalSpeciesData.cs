using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Models.Zoos;

namespace AnimalWorld.Data.Models.Animals
{
    public class AnimalSpeciesData : NamedBaseModel
    {
        public string Url { get; set; }

        public string NativeRange { get; set; }

        public string Description { get; set; }

        public int AnimalFamilyId { get; set; }

        public int CreatorId { get; set; }

        public AnimalFamilyData AnimalFamily { get; set; }

        public List<ZooData> Zoos { get; set; }

        public UserData Creator { get; set; }
    }
}
