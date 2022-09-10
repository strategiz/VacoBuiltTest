using System.Security.Cryptography;
using WebApiTest.BAL.Interfaces;
using WebApiTest.Constants;
using WebApiTest.DAL.Interfaces;
using WebApiTest.DTO.Posts;
using WebApiTest.Entities.Posts;

namespace WebApiTest.BAL.Implementations
{
    /// <summary>
    /// Implementation class for Blog Posts Services
    /// </summary>
    public class PostServices : IPostServices
    {
        private readonly ILogger<PostServices> _logger;
        private readonly IPostDA _postDA;

        public PostServices(ILogger<PostServices> logger, IPostDA postDA)
        {
            _logger = logger;
            _postDA = postDA;
        }

        /// <summary>
        /// Get all the posts in database
        /// </summary>
        /// <returns></returns>
        public List<PostInfo> GetAll()
        {
            var postsEnt = _postDA.GetAll();

            //Transform entity into desired return object
            return postsEnt.Select(p => new PostInfo
            {
                Id = p.Id,
                Timestamp = p.Timestamp,
                Title = p.Title,
                Text = p.Text,
                CategoryId = p.CategoryId,
                Category = GetCategories().First(c => c.Id == p.CategoryId)
            }).ToList();
        }

        /// <summary>
        /// Get a single post with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PostInfo GetById(int id)
        {
            var postEnt = _postDA.GetById(id);

            if (postEnt == null)
                return null;

            //Transform entity into desired return object
            return new PostInfo
            {
                Id = postEnt.Id,
                Timestamp = postEnt.Timestamp,
                Title = postEnt.Title,
                Text = postEnt.Text,
                CategoryId = postEnt.CategoryId,
                Category = GetCategories().First(c => c.Id == postEnt.CategoryId)
            };
        }

        /// <summary>
        /// Create a new blog post in the database
        /// </summary>
        /// <param name="postToCreate"></param>
        /// <returns>The created post</returns>
        public PostInfo Create(PostInfo postToCreate)
        {
            //Check if the post category exists
            if (!GetCategories().Any(c => c.Id == postToCreate.CategoryId))
            {
                _logger.LogWarning($"[POST SERVICE] The blog category '{postToCreate.CategoryId}' doesn't exists");
                return null;
            }

            var createdPostEnt = _postDA.Create(new PostEntity()
            {
                Timestamp = DateTime.Now,
                Title = postToCreate.Title,
                Text = postToCreate.Text,
                CategoryId = postToCreate.CategoryId
            });

            return new PostInfo()
            {
                Id = createdPostEnt.Id,
                Timestamp = createdPostEnt.Timestamp,
                Title = createdPostEnt.Title,
                Text = createdPostEnt.Text,
                CategoryId = createdPostEnt.CategoryId,
                Category = GetCategories().First(c => c.Id == createdPostEnt.CategoryId)
            };
        }

        /// <summary>
        /// Update a post
        /// </summary>
        /// <param name="postToUpdate"></param>
        /// <returns></returns>
        public PostInfo Update(PostInfo postToUpdate)
        {
            //Check if the post category exists
            if (!GetCategories().Any(c => c.Id == postToUpdate.CategoryId))
            {
                _logger.LogWarning($"[POST SERVICE] The blog category '{postToUpdate.CategoryId}' doesn't exists");
                return null;
            }

            var postsEnt = _postDA.GetAll();
            var postToUpdateEnt = postsEnt.FirstOrDefault(p => p.Id == postToUpdate.Id);

            if (postToUpdateEnt == null)
            {
                _logger.LogWarning($"[POST SERVICE] No post with id '{postToUpdate.Id}' has been found");
                return null;
            }

            postToUpdateEnt.Title = postToUpdate.Title;
            postToUpdateEnt.Text = postToUpdate.Text;
            postToUpdateEnt.CategoryId = postToUpdate.CategoryId;

            var updatedPostEnt = _postDA.Update(postToUpdateEnt);

            return new PostInfo()
            {
                Id = updatedPostEnt.Id,
                Timestamp = updatedPostEnt.Timestamp,
                Title = updatedPostEnt.Title,
                Text = updatedPostEnt.Text,
                CategoryId = updatedPostEnt.CategoryId,
                Category = GetCategories().First(c => c.Id == updatedPostEnt.CategoryId)
            };
        }

        /// <summary>
        /// Delete all the blog posts
        /// </summary>
        public void DeleteAll()
        {
            _postDA.DeleteByFilter();
        }

        /// <summary>
        /// Delete a blog post with a specific id
        /// </summary>
        /// <param name="id"></param>
        public bool DeleteById(int id)
        {
            return _postDA.DeleteByFilter(p => p.Id == id);
        }

        /// <summary>
        /// Returns the available categories
        /// </summary>
        /// <returns></returns>
        public List<BlogCategoryInfo> GetCategories()
        {
            //Call the database to retrieve the blog categories
            return BlogCategories.Categories;
        }
    }
}
