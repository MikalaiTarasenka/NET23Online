using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Web.Models.Users;

namespace AnimalWorld.Web.Mappers.Interfaces.Users
{
    public interface IAuthMapper
    {
        CredentialsDto Map(CredentialsViewModel source);
    }
}
