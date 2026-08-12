using AnimalWorld.Data.Models.User;

namespace AnimalWorld.Data.Models.Zoo
{
    public class CommentData : BaseModel
    {
        public int AuthorId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ZooId { get; set; }
        public virtual UserData Author { get; set; }
        public virtual ZooData Zoo { get; set; }
    }
}
