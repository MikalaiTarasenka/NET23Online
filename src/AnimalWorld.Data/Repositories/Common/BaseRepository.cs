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

        public virtual async Task<List<DataModel>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<DataModel> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task Create(DataModel model)
        {
            _dbSet.Add(model);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(DataModel model)
        {
            _dbSet.Update(model);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(int id)
        {
            var model = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (model != null)
            {
                _dbSet.Remove(model);
                await _context.SaveChangesAsync();
            }
        }
    }
}
