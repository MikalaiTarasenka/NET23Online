using AnimalWorld.Data.Models.Common;
using AnimalWorld.Data.Models.Users;

namespace AnimalWorld.Data.Models.Zoos
{
    public class CommentData : BaseModel
    {
        public int AuthorId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ZooId { get; set; }

        public UserData Author { get; set; }

        public ZooData Zoo { get; set; }
    }
}
