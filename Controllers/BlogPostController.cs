using blog.Dtos;
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
        public async Task<ActionResult<List<BlogPostDto>>> Get()
        {
            var result = await _blogRepository.Get();
            if (result == null)
            {
                return NoContent();
            }
            var postDtos = BlogPostDto.ListEntityToListDto(result);
            return Ok(postDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetById([FromRoute] int id)
        {
            var result = await _blogRepository.GetById(id);
            if (result == null)
            {
                return NoContent();
            }
            var postDto = BlogPostDto.EntityToDto(result);
            return Ok(postDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BlogPostCreateDto postDto)
        {
            var post = postDto.ToEntity();
            var result = await _blogRepository.Create(post);
            if (result == null)
            {
                return BadRequest();
            }
            var createdPostDto = BlogPostDto.EntityToDto(result);
            return Ok(createdPostDto);
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
