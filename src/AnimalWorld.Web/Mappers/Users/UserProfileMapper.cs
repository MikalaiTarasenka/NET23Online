using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Models.Users;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Animals;
using AnimalWorld.Web.Models.Users;

namespace AnimalWorld.Web.Mappers.Users
{
    public class UserProfileMapper : IReverseMapper<UserData, UserProfileViewModel>
    {
        public UserProfileViewModel Map(UserData source)
        {
            return new UserProfileViewModel
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                PhoneNumber = source.PhoneNumber,
                Language = source.Language
            };
        }

        public UserData ReverseMap(UserProfileViewModel destination)
        {
            return new UserData
            {
                FirstName = destination.FirstName,
                LastName = destination.LastName,
                PhoneNumber = destination.PhoneNumber,
                Language = destination.Language
            };
        }
    }
}
