using System.ComponentModel.DataAnnotations;

namespace AnimalWorld.Web.Models.Users
{
    public class CredentialsViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
