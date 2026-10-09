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

        private const int SHORT_DESCRIPTION_LENGTH = 150;

        public string ShortDescription
        {
            get
            {
                return Description.Length <= SHORT_DESCRIPTION_LENGTH
                    ? Description
                    : Description.Substring(0, SHORT_DESCRIPTION_LENGTH) + "...";
            }
        }

        public List<string>? AnimalFamilies { get; set; }
    }
}
