namespace AnimalWorld.Web.Models.Zoos
{
    public class CommentsViewModel
    {
        public ZooViewModel Zoo { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public int MAX_COMMENT_LENGTH => 1000;
    }
}
