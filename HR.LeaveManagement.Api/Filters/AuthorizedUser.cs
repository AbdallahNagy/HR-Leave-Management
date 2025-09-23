using Microsoft.AspNetCore.Mvc.Filters;

namespace HR.LeaveManagement.Api.Filters;

public class AuthorizedUser : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (user.Identity is { IsAuthenticated: false })
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.HttpContext.Response.CompleteAsync();
        }
    }
}