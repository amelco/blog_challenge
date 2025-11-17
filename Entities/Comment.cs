namespace blog.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public string Content { get; set; } = "";
    }
}
