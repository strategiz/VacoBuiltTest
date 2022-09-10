using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.BAL.Interfaces;
using WebApiTest.DTO.Users;

namespace WebApiTest.Controllers
{
    /// <summary>
    /// User controller : handle the user management
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserServices _userService;

        public UserController(ILogger<UserController> logger, IUserServices userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Get all the users in database
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the list of users</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_userService.GetAllUsers());
        }

        /// <summary>
        /// Get all the users in database
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return the user wanted</response>
        /// <response code="404">No user has been found</response>
        [HttpGet("{email}")]
        public IActionResult GetSingleUser(string email)
        {
            var user = _userService.GetUserByEmail(email);

            if (user != null)
                return new OkObjectResult(user);
            else
                return new NotFoundObjectResult($"The user with email '{email} doesn't exists");
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Successfully created the user</response>
        /// <response code="400">Fail to create the user</response>
        [HttpPost]
        public IActionResult Create(UserInfo userToCreate)
        {
            var isSuccess = _userService.CreateUser(userToCreate);

            if (isSuccess)
                return new CreatedResult(HttpContext.Request.GetDisplayUrl(), null);
            else
                return new BadRequestObjectResult("Fail to create the user");
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Successfully updated the user</response>
        /// <response code="400">Fail to update the user</response>
        [HttpPatch("{email}")]
        public IActionResult Update(string email, UserUpdateInfo userToUpdate)
        {
            var isSuccess = _userService.UpdateUser(email, userToUpdate);

            if (isSuccess)
                return new NoContentResult();
            else
                return new BadRequestObjectResult("Fail to update the user");
        }

        /// <summary>
        /// Delete a user by its email
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Successfully deleted the user</response>
        /// <response code="400">Fail to delete the user</response>
        [HttpDelete("{email}")]
        public IActionResult Delete(string email)
        {
            var isSuccess = _userService.DeleteUser(email);

            if (isSuccess)
                return new NoContentResult();
            else
                return new BadRequestObjectResult("Fail to delete the user");
        }
    }
}
