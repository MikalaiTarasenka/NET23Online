using AnimalWorld.Data.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalWorld.Web.Models.Users
{
    public class UserProfileViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public Language Language { get; set; }

        public List<SelectListItem> Languages { get; set; }
    }
}
