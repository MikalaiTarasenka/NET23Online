using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Zoos;

namespace AnimalWorld.Web.Mappers.Zoos
{
    public class CommentMapper : IMapper<CommentData, CommentViewModel>
    {
        public CommentViewModel Map(CommentData source)
        {
            return new CommentViewModel
            {
                AuthorName = source.Author.UserName,
                Text = source.Text,
                CreatedAt = source.CreatedAt.ToString("f")
            };
        }
    }
}
