using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Models.User;
using AnimalWorld.Data.Models.Zoo;

namespace AnimalWorld.Data.Models.Animal
{
    public class AnimalSpeciesData : NamedBaseModel
    {
        public string Url { get; set; }

        public string NativeRange { get; set; }

        public string Description { get; set; }

        public int AnimalFamilyId { get; set; }

        public int CreatorId { get; set; }

        public AnimalFamilyData AnimalFamily { get; set; }

        public List<ZooData> ZooData { get; set; }

        public UserData Creator { get; set; }
    }
}
