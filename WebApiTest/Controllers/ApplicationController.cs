using Microsoft.AspNetCore.Mvc;
using WebApiTest.DTO.Users;

namespace WebApiTest.Controllers
{
    /// <summary>
    /// Application controller, generic routes for the application
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ILogger<ApplicationController> _logger;
        private readonly IConfiguration _config;

        public ApplicationController(ILogger<ApplicationController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        /// <summary>
        /// This route allows to ping the web api and check that it is alive
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Api is alive</response>
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            _logger.LogDebug("[PING] Debug");
            _logger.LogInformation("[PING] Information");
            _logger.LogWarning("[PING] Warning");
            _logger.LogError("[PING] Error");

            return new OkResult();
        }

        /// <summary>
        /// Return the value of a single parameter
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <returns></returns>
        /// <response code="200">Return the value of the parameter</response>
        /// <response code="404">If the item is null</response>
        [HttpGet("parameter/{name}")]
        public IActionResult GetSingleParameter(string name)
        {
            var parameter = _config.GetValue<string>(name);

            if (!string.IsNullOrEmpty(parameter))
            {
                string message = $"The value of the parameter {name} is '{parameter}'";
                _logger.LogInformation(message);
                return new OkObjectResult(message);
            }
            else
            {
                string message = $"The parameter {name} has not been found";
                _logger.LogInformation(message);
                return new NotFoundObjectResult(message);
            }
        }
    }
}
