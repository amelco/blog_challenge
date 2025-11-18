using blog.Entities;

namespace blog.Dtos
{
    public class BlogPostDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }

        public virtual List<Comment>? Comments { get; set; }

        public BlogPost ToEntity()
        {
            return new BlogPost
            {
                Title = this.Title ?? "",
                Content = this.Content ?? "",
                Comments = this.Comments ?? new()
            };
        }

        public static BlogPostDto EntityToDto(BlogPost blogPost)
        {
            return new BlogPostDto
            {
                Title = blogPost.Title ?? null,
                Content = blogPost.Content ?? null,
                Comments = blogPost.Comments ?? null,
            };
        }

        public static List<BlogPostDto> ListEntityToListDto(List<BlogPost> blogPosts)
        {
            var postDtos = new List<BlogPostDto>();

            foreach (var post in blogPosts)
            {
                postDtos.Add(BlogPostDto.EntityToDto(post));
            }

            return postDtos;
        }
    }

    public class BlogPostCreateDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public BlogPost ToEntity()
        {
            return new BlogPost
            {
                Title = this.Title ?? "",
                Content = this.Content ?? "",
            };
        }
    }
}
