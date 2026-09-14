using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Interfaces.Common;

namespace AnimalWorld.Data.Repositories.Interfaces.Zoos
{
    public interface ICommentRepository : IBaseRepository<CommentData>
    {
        Task<List<CommentData>> GetZooComments(int zooId);
    }
}
