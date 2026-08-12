using AnimalWorld.Data.Models.User;

namespace AnimalWorld.Data.Models.Animal
{
    public class AnimalFamilyData : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public virtual List<AnimalSpeciesData> Species { get; set; }
        public virtual UserData Creator { get; set; }
    }
}
