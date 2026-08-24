using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Web.Mappers.Interfaces.Users;
using AnimalWorld.Web.Models.Users;

namespace AnimalWorld.Web.Mappers.Users
{
    public class AuthMapper : IAuthMapper
    {
        public CredentialsDto Map(CredentialsViewModel source)
        {
            return new CredentialsDto
            {
                UserName = source.UserName,
                Password = source.Password
            };
        }
    }
}
