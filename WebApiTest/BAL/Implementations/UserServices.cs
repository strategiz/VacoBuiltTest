using WebApiTest.BAL.Interfaces;
using WebApiTest.DAL.Interfaces;
using WebApiTest.DTO.Users;
using WebApiTest.Entities.Users;

namespace WebApiTest.BAL.Implementations
{
    /// <summary>
    /// Implementation class for User Services
    /// </summary>
    public class UserServices : IUserServices
    {
        private readonly ILogger<UserServices> _logger;
        private readonly IUserDA _userDA;

        public UserServices(ILogger<UserServices> logger, IUserDA userDA)
        {
            _logger = logger;
            _userDA = userDA;
        }

        /// <summary>
        /// Get all the user in database
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetAllUsers()
        {
            var userEnt = _userDA.GetAllUsers();
            return userEnt.Select(u => new UserInfo
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                BirthDate = u.BirthDate
            }).ToList();
        }

        /// <summary>
        /// Get a single user given its email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserInfo GetUserByEmail(string email)
        {
            UserInfo userInfo = null;
            var userEnt = _userDA.GetUserByFilter(u => u.Email == email);

            if (userEnt != null)
            {
                userInfo = new UserInfo
                {
                    Email = userEnt.Email,
                    FirstName = userEnt.FirstName,
                    LastName = userEnt.LastName,
                    BirthDate = userEnt.BirthDate
                };
            }

            return userInfo;
        }

        /// <summary>
        /// Create a new user in the database
        /// </summary>
        /// <param name="userToCreate"></param>
        /// <returns></returns>
        public bool CreateUser(UserInfo userToCreate)
        {
            var users = _userDA.GetAllUsers();

            //Check if a user have the same email address
            if (users.Any(u => u.Email == userToCreate.Email))
            {
                _logger.LogWarning($"[USER SERVICE] A user with the mail '{userToCreate.Email}' already exists");
                return false;
            }

            _userDA.CreateUser(new UserEntity()
            {
                Guid = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                Email = userToCreate.Email,
                BirthDate = userToCreate.BirthDate
            });

            return true;
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userToUpdate"></param>
        /// <returns></returns>
        public bool UpdateUser(string email, UserUpdateInfo userToUpdate)
        {
            //Get the user to update
            var userEnt = _userDA.GetUserByFilter(u => u.Email == email);

            //Check if the user exists
            if (userEnt is null)
            {
                _logger.LogWarning($"[USER SERVICE] No user with email '{email}' exists");
                return false;
            }

            //Update values
            if (!string.IsNullOrEmpty(userToUpdate.FirstName))
                userEnt.FirstName = userToUpdate.FirstName;
            if (!string.IsNullOrEmpty(userToUpdate.LastName))
                userEnt.LastName = userToUpdate.LastName;
            if (userToUpdate.BirthDate.HasValue)
                userEnt.BirthDate = userToUpdate.BirthDate;

            _userDA.UpdateUser(userEnt);

            return true;
        }

        /// <summary>
        /// Delete a user in database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool DeleteUser(string email)
        {
            var userToDelete = _userDA.GetUserByFilter(u => u.Email == email);

            if (userToDelete != null)
            {
                _userDA.DeleteUser(userToDelete);
                _logger.LogInformation($"[USER SERVICE] the user '{email}' has been deleted");
                return true;
            }
            else
            {
                _logger.LogWarning($"[USER SERVICE] impossible to delete the user '{email}' because it doesn't exists");
                return false;
            }
        }
    }
}
