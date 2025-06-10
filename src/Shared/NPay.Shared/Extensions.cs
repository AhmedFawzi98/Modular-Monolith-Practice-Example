using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NPay.Shared.Commands;
using NPay.Shared.Database;
using NPay.Shared.Dispatchers;
using NPay.Shared.Events;
using NPay.Shared.Exceptions;
using NPay.Shared.Messaging;
using NPay.Shared.Queries;
using NPay.Shared.Time;

namespace NPay.Shared;

public static class Extensions
{
      
    public static IServiceCollection AddSharedFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddErrorHandling();
        services.AddCommands();
        services.AddEvents();
        services.AddQueries();
        services.AddMessaging();
        services.AddPostgres(configuration);
        services.AddSingleton<IClock, UtcClock>();
        services.AddSingleton<IDispatcher, InMemoryDispatcher>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
        return services;
    }
        
    public static IApplicationBuilder UseSharedFramework(this IApplicationBuilder app)
    {
        app.UseErrorHandling();
        return app;
    }
}