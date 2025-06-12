using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NPay.Shared.Database;
using NPay.Shared.Events;
using NPay.Shared.Exceptions;
using NPay.Shared.Messaging;
using NPay.Shared.Time;
using System.Reflection;

namespace NPay.Shared;

public static class Extensions
{
      
    public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddErrorHandling();
        services.AddPostgresOptionsAndDBInitalizer(configuration);
        services.AddSingleton<IClock, UtcClock>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
        return services;
    }
        
    public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
    {
        app.UseErrorHandling();
        return app;
    }
}