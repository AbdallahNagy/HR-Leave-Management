using HR.LeaveManagement.Api.Filters;

namespace HR.LeaveManagement.Api;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<AuthorizedUser>();
        services.AddScoped<ResourceFilter>();

        return services;
    }
}