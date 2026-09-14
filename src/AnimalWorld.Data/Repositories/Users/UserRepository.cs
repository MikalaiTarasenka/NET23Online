using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Repositories.Common;
using AnimalWorld.Data.Repositories.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace AnimalWorld.Data.Repositories.Users
{
    internal class UserRepository : BaseRepository<UserData>, IUserRepository
    {
        public UserRepository(WebContext context) : base(context) { }

        public async Task<bool> UserNameIsFree(string userName)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.UserName == userName) == null;
        }

        public async Task<UserData> GetUser(string userName)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
