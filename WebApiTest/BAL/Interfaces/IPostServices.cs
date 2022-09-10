using WebApiTest.DTO.Posts;

namespace WebApiTest.BAL.Interfaces
{
    /// <summary>
    /// Interface for the Blog Posts Services
    /// </summary>
    public interface IPostServices
    {
        /// <summary>
        /// Get all the post in database
        /// </summary>
        /// <returns></returns>
        List<PostInfo> GetAll();

        /// <summary>
        /// Get a single post with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PostInfo GetById(int id);

        /// <summary>
        /// Create a new blog post in the database
        /// </summary>
        /// <param name="postToCreate"></param>
        /// <returns>The created post</returns>
        PostInfo Create(PostInfo postToCreate);

        /// <summary>
        /// Update a post
        /// </summary>
        /// <param name="postToUpdate"></param>
        /// <returns></returns>
        PostInfo Update(PostInfo postToUpdate);

        /// <summary>
        /// Delete all the blog posts
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Delete a blog post with a specific id
        /// </summary>
        /// <param name="id"></param>
        bool DeleteById(int id);

        /// <summary>
        /// Returns the available categories
        /// </summary>
        /// <returns></returns>
        List<BlogCategoryInfo> GetCategories();
    }
}
