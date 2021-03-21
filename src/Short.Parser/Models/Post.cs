using System;

namespace Short.Parser
{
    public class Post
    {
        public PostMetadata Metadata { get; set; } = new PostMetadata();
        public string Content { get; set; }
    }

    public class PostMetadata
    {
        public string Title { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string Author { get; set; }

    }
}
