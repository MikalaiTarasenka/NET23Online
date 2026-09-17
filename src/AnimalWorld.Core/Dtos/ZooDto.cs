namespace AnimalWorld.Core.Dtos
{
    public class ZooDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public List<string> AnimalFamilies { get; set; }
    }
}
