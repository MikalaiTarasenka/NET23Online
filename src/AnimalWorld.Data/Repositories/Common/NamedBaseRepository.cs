using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Repositories.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace AnimalWorld.Data.Repositories.Common
{
    internal abstract class NamedBaseRepository<DataModel> : BaseRepository<DataModel>, INamedBaseRepository<DataModel> where DataModel : NamedBaseModel
    {
        public NamedBaseRepository(WebContext context) : base(context) { }

        public virtual async Task<DataModel> GetByName(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
