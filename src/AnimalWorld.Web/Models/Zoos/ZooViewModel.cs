using System.ComponentModel.DataAnnotations;

namespace AnimalWorld.Web.Models.Zoos
{
    public class ZooViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        public List<string> AnimalFamilies { get; set; }
    }
}
