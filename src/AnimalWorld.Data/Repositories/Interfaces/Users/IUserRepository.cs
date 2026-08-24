using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Repositories.Interfaces.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Users
{
    public interface IUserRepository : IBaseRepository<UserData>
    {
        bool UserNameIsFree(string userName);

        UserData GetUser(string userName);
    }
}
