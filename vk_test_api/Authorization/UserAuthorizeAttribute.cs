namespace vk_test_api.Authorization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using vk_test_api.Data.Models;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class UserAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var user = (User)context.HttpContext.Items["User"];
        if (user == null)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

            // set 'WWW-Authenticate' header to trigger login popup in browsers
            context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
        }
    }
}
