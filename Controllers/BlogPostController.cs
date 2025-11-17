using blog.Entities;
using blog.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class BlogPostController : ControllerBase
    {
        private readonly IBasicRepository<BlogPost> _blogRepository;
        private readonly IBasicRepository<Comment> _commentRepository;

        public BlogPostController(IBasicRepository<BlogPost> blogRepository, IBasicRepository<Comment> commentRepository)
        {
            _blogRepository = blogRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> Get()
        {
            var result = await _blogRepository.Get();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<BlogPost>> GetById([FromRoute] int id)
        {
            var result = await _blogRepository.GetById(id);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BlogPost post)
        {
            var result = await _blogRepository.Create(post);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<ActionResult> Post([FromRoute] int id, [FromBody] string message)
        {
            var comment = new Comment
            {
                BlogPostId = id,
                Content = message
            };

            var result = await _commentRepository.Create(comment);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
