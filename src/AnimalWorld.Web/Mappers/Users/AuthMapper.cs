using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Users;

namespace AnimalWorld.Web.Mappers.Users
{
    public class AuthMapper : IMapper<CredentialsViewModel, CredentialsDto>
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
