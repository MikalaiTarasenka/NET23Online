using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Repositories.Interfaces.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Users
{
    public interface IUserRepository : IBaseRepository<UserData>
    {
        Task<bool> UserNameIsFree(string userName);

        Task<UserData> GetUser(string userName);
    }
}
