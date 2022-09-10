using WebApiTest.DTO.Posts;
using WebApiTest.Entities.Posts;

namespace WebApiTest.DAL.Interfaces
{
    /// <summary>
    /// Interface for Blog Posts Data Access
    /// </summary>
    public interface IPostDA
    {
        /// <summary>
        /// Get all the posts in the database
        /// </summary>
        /// <returns></returns>
        List<PostEntity> GetAll();

        /// <summary>
        /// Get a single post with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PostEntity GetById(int id);

        /// <summary>
        /// Add a single post to the database
        /// </summary>
        /// <param name="postToCreate"></param>
        PostEntity Create(PostEntity postToCreate);

        /// <summary>
        /// Update a post
        /// </summary>
        /// <param name="postToUpdate"></param>
        /// <returns></returns>
        PostEntity Update(PostEntity postToUpdate);

        /// <summary>
        /// Delete posts with a specific filter
        /// If no filter is given, all the posts are deleted
        /// </summary>
        /// <returns></returns>
        bool DeleteByFilter(Predicate<PostEntity> filter = null);
    }
}
