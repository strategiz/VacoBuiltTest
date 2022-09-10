using System.ComponentModel.DataAnnotations;

namespace WebApiTest.DTO.Posts
{
    public class PostInfo
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
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// COntent of the blog post
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Id of the category of the blog post
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Category object corresponding to the given id
        /// </summary>
        public BlogCategoryInfo Category { get; set; }
    }
}
