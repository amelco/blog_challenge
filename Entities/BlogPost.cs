namespace blog.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";

        public virtual List<Comment> Comments { get; set; } = new();
    }
}
