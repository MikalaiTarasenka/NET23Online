using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Mappers.Interfaces.CustomMappers;
using AnimalWorld.Web.Models.Zoos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers.Zoos
{
    [Authorize]
    public class CommentController : Controller
    {
        private ICommentService _commentService;
        private IMapper<CommentData, CommentViewModel> _commentMapper;
        private IZooService _zooService;
        private IZooMapper _zooMapper;

        public CommentController(ICommentService commentService, IMapper<CommentData, CommentViewModel> commentMapper, IZooService zooService, IZooMapper zooMapper)
        {
            _commentService = commentService;
            _commentMapper = commentMapper;
            _zooService = zooService;
            _zooMapper = zooMapper;
        }

        public async Task<IActionResult> ZooComments(int zooId)
        {
            var commentDatas = await _commentService.GetZooComments(zooId);
            var commentViewModel = _commentMapper.MapList(commentDatas);
            var zooData = await _zooService.Get(zooId);
            var zooViewModel = _zooMapper.Map(zooData);
            var viewModel = new CommentsViewModel
            {
                Zoo = zooViewModel,
                Comments = commentViewModel
            };
            return View(viewModel);
        }
    }
}
