using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.BAL.Implementations;
using WebApiTest.BAL.Interfaces;
using WebApiTest.Constants;
using WebApiTest.DTO.Posts;

namespace WebApiTest.Controllers
{
    /// <summary>
    /// Posts controller : endpoint for blog posts operations
    /// </summary>
    [ApiController]
    public class PostsController : Controller
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IPostServices _postService;

        public PostsController(ILogger<PostsController> logger, IPostServices postService)
        {
            _logger = logger;
            _postService = postService;
        }

        /// <summary>
        /// Returns all blog posts in JSON format. Posts are ordered from most recent to least recent.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns all blog posts in JSON format. Posts are ordered from most recent to least recent.</response>
        [HttpGet("posts")]
        public IActionResult GetAll()
        {
            return new OkObjectResult(_postService.GetAll());
        }

        /// <summary>
        /// Returns a single blog post in JSON format corresponding to the provided ID
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns a single blog post in JSON format corresponding to the provided ID</response>
        /// <response code="400">Failed to retrieve the post</response>
        [HttpGet("posts/{id}")]
        public IActionResult GetById(int id)
        {
            var post = _postService.GetById(id);

            if (post != null)
                return new OkObjectResult(post);
            else
                return new BadRequestObjectResult($"Failed to retrieve post with id '{id}'");
        }

        /// <summary>
        /// Create a new blog post
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the post that was created</response>
        /// <response code="400">Failed to create the post</response>
        [HttpPost("posts")]
        public IActionResult Create(PostInfo postToCreate)
        {
            var createdPost = _postService.Create(postToCreate);

            if (createdPost != null)
                return new CreatedResult(HttpContext.Request.GetDisplayUrl(), createdPost);
            else
                return new BadRequestObjectResult("Fail to create the user");
        }

        /// <summary>
        /// Deletes all the posts in the blog
        /// </summary>
        /// <returns></returns>
        /// <response code="204">All the blog posts have been deleted</response>
        [HttpDelete("posts")]
        public IActionResult DeleteAll()
        {
            _postService.DeleteAll();
            return new NoContentResult();
        }

        /// <summary>
        /// Deletes the blog post that matches the provided ID
        /// </summary>
        /// <returns></returns>
        /// <response code="204">The blog post has been deleted</response>
        /// <response code="400">Failed to delete the blog post</response>
        [HttpDelete("posts/{id}")]
        public IActionResult DeletePostById(int id)
        {
            if (_postService.DeleteById(id))
                return new NoContentResult();
            else
                return new BadRequestObjectResult($"Failed to delete the post with id {id}");
        }

        /// <summary>
        /// Updates an existing blog post, corresponding to the provided ID
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the updated post</response>
        /// <response code="400">Failed to update the blog post</response>
        [HttpPut("posts")]
        public IActionResult Update(PostInfo postToUpdate)
        {
            var updatedPost = _postService.Update(postToUpdate);

            if (updatedPost != null)
                return new OkObjectResult(updatedPost);
            else
                return new BadRequestObjectResult($"Failed to update the post with id {postToUpdate.Id}");
        }

        /// <summary>
        /// Returns the available categories
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the available categories</response>
        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            return new OkObjectResult(_postService.GetCategories());
        }
    }
}
