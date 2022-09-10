using System.Text.Json;
using WebApiTest.DAL.Interfaces;
using WebApiTest.DTO.Posts;
using WebApiTest.Entities.Posts;

namespace WebApiTest.DAL.Implementations
{
    /// <summary>
    /// Simple Data Access Layer for Blog Posts
    /// The database is represented by a simple json file
    /// </summary>
    public class PostSimpleDAL : IPostDA
    {
        private readonly ILogger<PostSimpleDAL> _logger;

        private readonly string _fileLocation;

        public PostSimpleDAL(ILogger<PostSimpleDAL> logger, IConfiguration config)
        {
            _logger = logger;
            _fileLocation = config.GetValue<string>("PostDatabaseLocation");

            if (string.IsNullOrEmpty(_fileLocation))
                throw new Exception("The database for Users is not define in the appsettings.json file");

            if (!File.Exists(_fileLocation))
                File.WriteAllText(_fileLocation, "[]");
        }

        /// <summary>
        /// Get all the posts in the database
        /// </summary>
        /// <returns></returns>
        public List<PostEntity> GetAll()
        {
            var json = File.ReadAllText(_fileLocation);
            var posts = JsonSerializer.Deserialize<List<PostEntity>>(json);

            _logger.LogDebug($"[POST SIMPLE DAL] {posts.Count} user(s) retrieved");

            return posts;
        }

        /// <summary>
        /// Get a single post with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PostEntity GetById(int id)
        {
            var posts = GetAll();
            return posts.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Add a single post to the database
        /// </summary>
        /// <param name="postToCreate"></param>
        public PostEntity Create(PostEntity postToCreate)
        {
            var posts = GetAll();

            //Comptue the id of the new post
            //This doesn't ensure that the id is auto incremented but this allow to have a unique id for each blog post
            if (posts.Any())
                postToCreate.Id = posts.Max(p => p.Id) + 1;
            else
                postToCreate.Id = 1;

            posts.Add(postToCreate);
            WriteIntoDatabase(posts);

            _logger.LogInformation($"[POST SIMPLE DAL] Successfully created the post '{postToCreate.Title} with id '{postToCreate.Title}'");
            return postToCreate;
        }

        /// <summary>
        /// Update a post
        /// </summary>
        /// <param name="postToUpdate"></param>
        /// <returns></returns>
        public PostEntity Update(PostEntity postToUpdate)
        {
            var posts = GetAll();

            //For this "simple" implementation of a database it's easier to remove the data and to insert it again
            posts.RemoveAll(u => u.Id == postToUpdate.Id);
            posts.Add(postToUpdate);

            WriteIntoDatabase(posts);

            return postToUpdate;
        }

        /// <summary>
        /// Delete posts with a specific filter
        /// If no filter is given, all the posts are deleted
        /// </summary>
        /// <returns></returns>
        public bool DeleteByFilter(Predicate<PostEntity> filter)
        {
            if (filter == null)
            {//If filter is null, simply delete all the posts
                File.WriteAllText(_fileLocation, "[]");
                return true;
            }

            var posts = GetAll();
            var removedCount = posts.RemoveAll(filter);

            if (removedCount > 0)
            {
                WriteIntoDatabase(posts);
                _logger.LogDebug($"[POST SIMPLE DAL] {removedCount} post(s) has been removed");
                return true;
            }
            else
            {
                _logger.LogDebug("[POST SIMPLE DAL] no posts match for deletion");
                return false;
            }
        }

        /// <summary>
        /// Write the data into the "database"
        /// </summary>
        /// <param name="posts"></param>
        private void WriteIntoDatabase(List<PostEntity> posts)
        {
            posts = posts.OrderBy(u => u.Timestamp).ToList();
            File.WriteAllText(_fileLocation, JsonSerializer.Serialize(posts));
        }
    }
}
