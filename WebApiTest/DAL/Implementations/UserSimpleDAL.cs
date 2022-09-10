using System.Text.Json;
using WebApiTest.DAL.Interfaces;
using WebApiTest.Entities.Users;

namespace WebApiTest.DAL.Implementations
{
    /// <summary>
    /// Simple Data Access Layer for Users
    /// The database is represented by a simple json file
    /// </summary>
    public class UserSimpleDAL : IUserDA
    {
        private readonly ILogger<UserSimpleDAL> _logger;

        private readonly string _fileLocation;

        public UserSimpleDAL(ILogger<UserSimpleDAL> logger, IConfiguration config)
        {
            _logger = logger;
            _fileLocation = config.GetValue<string>("UserDatabaseLocation");

            if (string.IsNullOrEmpty(_fileLocation))
                throw new Exception("The database for Users is not define in the appsettings.json file");

            if (!File.Exists(_fileLocation))
                File.WriteAllText(_fileLocation, "[]");
        }

        /// <summary>
        /// Get all the users in the database
        /// </summary>
        /// <returns></returns>
        public List<UserEntity> GetAllUsers()
        {
            var json = File.ReadAllText(_fileLocation);
            var users = JsonSerializer.Deserialize<List<UserEntity>>(json);

            _logger.LogDebug($"[USER SIMPLE DAL] {users.Count} user(s) retrieved");

            return users;
        }

        /// <summary>
        /// Get a single user given a filter
        /// </summary>
        /// <returns></returns>
        public UserEntity GetUserByFilter(Func<UserEntity, bool> filter)
        {
            var json = File.ReadAllText(_fileLocation);
            var users = JsonSerializer.Deserialize<List<UserEntity>>(json);

            var user = users.FirstOrDefault(filter);

            if (user != null)
                _logger.LogDebug("[USER SIMPLE DAL] a user match the filter");
            else
                _logger.LogDebug("[USER SIMPLE DAL] no user found");

            return user;
        }

        /// <summary>
        /// Add a single user to the database
        /// </summary>
        /// <param name="userToCreate"></param>
        public void CreateUser(UserEntity userToCreate)
        {
            var users = GetAllUsers();
            users.Add(userToCreate);
            WriteIntoDatabase(users);

            _logger.LogInformation($"[USER SIMPLE DAL] Successfully created the user with email '{userToCreate}'");
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="userToUpdate"></param>
        /// <returns></returns>
        public void UpdateUser(UserEntity userToUpdate)
        {
            var users = GetAllUsers();

            //For this "simple" implementation of a database it's easier to remove the data and to insert it again
            users.RemoveAll(u => u.Guid == userToUpdate.Guid);
            userToUpdate.UpdatedAt = DateTimeOffset.Now;
            users.Add(userToUpdate);

            WriteIntoDatabase(users);
        }

        /// <summary>
        /// Delete a single user from the database
        /// </summary>
        /// <param name="userToDelete"></param>
        public void DeleteUser(UserEntity userToDelete)
        {
            var users = GetAllUsers();
            users.RemoveAll(u => u.Guid == userToDelete.Guid);
            WriteIntoDatabase(users);
        }

        /// <summary>
        /// Write the data into the "database"
        /// </summary>
        /// <param name="users"></param>
        private void WriteIntoDatabase(List<UserEntity> users)
        {
            users = users.OrderBy(u => u.CreatedAt).ToList();
            File.WriteAllText(_fileLocation, JsonSerializer.Serialize(users));
        }
    }
}
