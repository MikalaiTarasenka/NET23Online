using System.ComponentModel.DataAnnotations;

namespace AnimalWorld.Web.Models.Animals
{
    public class AnimalFamilyViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
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
    }
}
