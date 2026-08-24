using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Repositories.Common;
using AnimalWorld.Data.Repositories.Interfaces.Users;

namespace AnimalWorld.Data.Repositories.Users
{
    internal class UserRepository : BaseRepository<UserData>, IUserRepository
    {
        public UserRepository(WebContext context) : base(context) { }

        public bool UserNameIsFree(string userName)
        {
            return _dbSet.FirstOrDefault(x => x.UserName == userName) == null;
        }

        public UserData GetUser(string userName)
        {
            return _dbSet.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
