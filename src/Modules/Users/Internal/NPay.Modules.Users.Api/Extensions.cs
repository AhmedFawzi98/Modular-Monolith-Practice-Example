using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NPay.Modules.Users.Core;

namespace NPay.Modules.Users.Api;

public static class Extensions
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCoreLayer(configuration);
            
        return services;
    }
        
    public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
    {
        return app;
    }
}