using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace HR.LeaveManagement.Api.Filters;

public class ResourceFilter(IMemoryCache cache) : IResourceFilter
{
    private const int CacheDurationInMinutes = 5;

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        var cacheKey = context.HttpContext.Request.Path.ToString();
        if (cache.TryGetValue(cacheKey, out var cachedResponse))
        {
            context.Result = (IActionResult)cachedResponse!;
        }
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        var cacheKey = context.HttpContext.Request.Path.ToString();
        if (context.Result == null) return;
        
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheDurationInMinutes)
        };
        
        cache.Set(cacheKey, context.Result, cacheEntryOptions);
    }
}