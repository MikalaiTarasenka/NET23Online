using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Repositories.Interfaces.Common;

namespace AnimalWorld.Data.Repositories.Common
{
    internal class NamedBaseRepository<DataModel> : BaseRepository<DataModel>, INamedBaseRepository<DataModel> where DataModel : NamedBaseModel
    {
        public NamedBaseRepository(WebContext context) : base(context) { }

        public virtual DataModel GetByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.Name == name);
        }
    }
}
