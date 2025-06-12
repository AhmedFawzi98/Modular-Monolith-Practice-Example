using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NPay.Shared.Events;
using System;
using System.Reflection;

namespace NPay.Shared.Messaging;

public static class Extensions
{        
    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IBusRegistrationConfigurator> configureModules) 
    {
        services.Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.RabbitMq));

        var rabbitMqSettings = configuration.GetSection(RabbitMqSettings.RabbitMq).Get<RabbitMqSettings>();
        services.AddMassTransit(busconfig =>
        {
            busconfig.SetKebabCaseEndpointNameFormatter();

            configureModules(busconfig);


            busconfig.UsingRabbitMq((context, rabbitMqConfig) =>
            {
                rabbitMqConfig.Host(new Uri(rabbitMqSettings.HostAddress), hostConfig =>
                {
                    hostConfig.Username(rabbitMqSettings.Username);
                    hostConfig.Password(rabbitMqSettings.Password);
                    hostConfig.ConnectionName(rabbitMqSettings.ConnectionName);
                });

                rabbitMqConfig.ConfigureEndpoints(context);

            });
        });

        services.AddScoped<IEventPublisher, EventPublisher>();

        return services;
    }
}