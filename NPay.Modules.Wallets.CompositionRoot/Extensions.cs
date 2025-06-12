using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NPay.Modules.Wallets.Application;
using NPay.Modules.Wallets.Core;
using NPay.Modules.Wallets.Infrastructure;
using NPay.Shared.Messaging;
using System.Reflection;

namespace NPay.Modules.Wallets.CompositionRoot;

public static class Extensions
{
    public static IServiceCollection AddWalletsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCoreLayer();
        services.AddApplicationLayer();
        services.AddInfrastructureLayer(configuration);

        return services;
    }

    public static IApplicationBuilder UseWalletsModule(this IApplicationBuilder app)
    {
        return app;
    }
}