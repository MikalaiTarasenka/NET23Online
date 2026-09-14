using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Common;
using AnimalWorld.Data.Repositories.Interfaces.Zoos;
using Microsoft.EntityFrameworkCore;

namespace AnimalWorld.Data.Repositories.Zoos
{
    internal class CommentRepository : BaseRepository<CommentData>, ICommentRepository
    {
        public CommentRepository(WebContext context) : base(context) { }

        public async Task<List<CommentData>> GetZooComments(int zooId)
        {
            return await _dbSet
                .Include(x => x.Author)
                .Where(x => x.ZooId == zooId)
                .ToListAsync();
        }
    }
}
