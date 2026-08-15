using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Repositories.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace AnimalWorld.Data.Repositories.Common
{
    internal abstract class BaseRepository<DataModel> : IBaseRepository<DataModel> where DataModel : BaseModel
    {
        protected WebContext _context;
        protected DbSet<DataModel> _dbSet;

        protected BaseRepository(WebContext context)
        {
            _context = context;
            _dbSet = _context.Set<DataModel>();
        }

        public virtual List<DataModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual DataModel GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public virtual void Create(DataModel model)
        {
            _dbSet.Add(model);
            _context.SaveChanges();
        }

        public virtual void Update(DataModel model)
        {
            _dbSet.Update(model);
            _context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var model = _dbSet.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                _dbSet.Remove(model);
                _context.SaveChanges();
            }
        }
    }
}
