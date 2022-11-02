namespace Blog.Models
{
    public class User
    {
        public string Id { get; set; }

        public IList<Post> Posts { get; set; }
        public IList<Role> Roles { get; set; }
    }
}