using AnimalWorld.Core.Services.Interfaces.Zoos;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Models.Zoos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalWorld.Web.Controllers.Zoos
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CommentApiController : ControllerBase
    {
        private ICommentService _commentService;
        private IMapper<CommentData, CommentViewModel> _commentMapper;

        public CommentApiController(ICommentService commentService, IMapper<CommentData, CommentViewModel> commentMapper)
        {
            _commentService = commentService;
            _commentMapper = commentMapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromForm(Name = "ZooId")] int zooId, [FromForm(Name = "CommentText")] string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest();
            }

            var commentData = await _commentService.AddZooComment(zooId, text);
            var commentViewModel = _commentMapper.Map(commentData);
            return Ok(commentViewModel);
        }
    }
}
