using AnimalWorld.Data.Models.Zoos;

namespace AnimalWorld.Core.Services.Interfaces.Zoos
{
    public interface ICommentService
    {
        public Task<List<CommentData>> GetZooComments(int zooId);

        public Task<CommentData> AddZooComment(int zooId, string text);
    }
}
