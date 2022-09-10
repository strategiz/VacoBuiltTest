using WebApiTest.Entities.Users;

namespace WebApiTest.DAL.Interfaces
{
    /// <summary>
    /// Interface for User Data Access
    /// </summary>
    public interface IUserDA
    {
        /// <summary>
        /// Get all the users in the database
        /// </summary>
        /// <returns></returns>
        List<UserEntity> GetAllUsers();

        /// <summary>
        /// Get a single user given a filter
        /// </summary>
        /// <returns></returns>
        UserEntity GetUserByFilter(Func<UserEntity, bool> filter);

        /// <summary>
        /// Add a single user to the database
        /// </summary>
        /// <param name="userToCreate"></param>
        void CreateUser(UserEntity userToCreate);

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="userToUpdate"></param>
        /// <returns></returns>
        void UpdateUser(UserEntity userToUpdate);

        /// <summary>
        /// Delete a single user from the database
        /// </summary>
        /// <param name="userToDelete"></param>
        void DeleteUser(UserEntity userToDelete);
    }
}
