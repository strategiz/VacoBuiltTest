namespace WebApiTest.Entities.Posts
{
    public class PostEntity
    {
        /// <summary>
        /// Auto assigned id (and unique among the existing blog posts)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Datetime of the creation of the post (in UTC)
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Title of the blog post
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// COntent of the blog post
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Id of the category of the blog post
        /// </summary>
        public int CategoryId { get; set; }
    }
}
