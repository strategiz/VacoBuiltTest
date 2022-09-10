using WebApiTest.DTO.Users;

namespace WebApiTest.BAL.Interfaces
{
    /// <summary>
    /// Interface for the User Services
    /// </summary>
    public interface IUserServices
    {
        /// <summary>
        /// Get all the user in database
        /// </summary>
        /// <returns></returns>
        List<UserInfo> GetAllUsers();

        /// <summary>
        /// Get a single user given its email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        UserInfo GetUserByEmail(string email);

        /// <summary>
        /// Create a new user in the database
        /// </summary>
        /// <param name="userToCreate"></param>
        /// <returns></returns>
        bool CreateUser(UserInfo userToCreate);

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userToUpdate"></param>
        /// <returns></returns>
        bool UpdateUser(string email, UserUpdateInfo userToUpdate);

        /// <summary>
        /// Delete a user in database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool DeleteUser(string email);
    }
}
