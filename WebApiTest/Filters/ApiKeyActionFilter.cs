using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTest.Filters
{
    /// <summary>
    /// Filter to make a minimal securization of the web api
    /// </summary>
    public class ApiKeyActionFilter : IAsyncActionFilter
    {
        private const string API_KEY_NAME = "api-key";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Check if the api key is present in the request
            if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_NAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "The Api Key is required to access those apis"
                };
                return;
            }

            //Check is the api key is valid
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(API_KEY_NAME);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "The provided Api Key is not valid"
                };
                return;
            }
            await next();
        }
    }
}
