using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NPay.Modules.Notifications.Api.Services;
using NPay.Modules.Notifications.Shared;
using NPay.Shared.Messaging;
using System.Reflection;

namespace NPay.Modules.Notifications.Api;

public static class Extensions
{
    public static IServiceCollection AddNotificationsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<INotificationsModuleApi, NotificationsModuleApi>();
        services.AddSingleton<IEmailSender, EmailSender>();
        services.AddSingleton<IEmailResolver, EmailResolver>();

        return services;
    }
        
    public static IApplicationBuilder UseNotificationsModule(this IApplicationBuilder app)
    {
        return app;
    }

    public static IBusRegistrationConfigurator ConfigureMasstrasnitForNotificationsModule(this IBusRegistrationConfigurator busconfig)
    {
        var assembly = Assembly.GetExecutingAssembly();
        busconfig.AddConsumers(assembly);

        return busconfig;
    }
}