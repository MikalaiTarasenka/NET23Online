using AnimalWorld.Data.Models.User;
using AnimalWorld.Data.Models.Zoo;

namespace AnimalWorld.Data.Models.Animal
{
    public class AnimalSpeciesData : BaseModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string NativeRange { get; set; }
        public string Description { get; set; }
        public int AnimalFamilyId { get; set; }
        public int CreatorId { get; set; }
        public virtual AnimalFamilyData AnimalFamily { get; set; }
        public virtual List<ZooData> ZooData { get; set; }
        public virtual UserData Creator { get; set; }
    }
}
