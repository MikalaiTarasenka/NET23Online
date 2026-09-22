using AnimalWorld.Core.Services.Interfaces.Users;
using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Data.Repositories.Interfaces.Zoos;

namespace AnimalWorld.Core.Services.Zoos
{
    internal class CommentService : ICommentService
    {
        private ICommentRepository _commentRepository;
        private IAuthService _authService;

        public CommentService(ICommentRepository commentRepository, IAuthService authService)
        {
            _commentRepository = commentRepository;
            _authService = authService;
        }

        public async Task<List<CommentData>> GetZooComments(int zooId)
        {
            return await _commentRepository.GetZooComments(zooId);
        }

        public async Task<CommentData> AddZooComment(int zooId, string text)
        {
            var user = await _authService.GetUser();
            var date = DateTime.UtcNow;
            var commentData = new CommentData
            {
                Author = user,
                AuthorId = user.Id,
                CreatedAt = date,
                ZooId = zooId,
                Text = text
            };
            await _commentRepository.Create(commentData);
            return commentData;
        }
    }
}
