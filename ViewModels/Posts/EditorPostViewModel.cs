using Blog.Models;

namespace BlogNet.ViewModels.Posts
{
    public class EditorPostViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Category Category { get; set; }
        public string AuthorId { get; set; }
    }
}
