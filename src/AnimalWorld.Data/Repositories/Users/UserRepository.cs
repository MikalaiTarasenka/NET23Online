using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Repositories.Common;
using AnimalWorld.Data.Repositories.Interfaces.Users;

namespace AnimalWorld.Data.Repositories.Users
{
    internal class UserRepository : BaseRepository<UserData>, IUserRepository
    {
        public UserRepository(WebContext context) : base(context) { }

        public bool LoginIsFree(string login)
        {
            return _dbSet.FirstOrDefault(x => x.Login == login) == null;
        }
    }
}
