using Microsoft.Extensions.DependencyInjection;
using NPay.Modules.Wallets.Application.Services;
using NPay.Modules.Wallets.Shared;
using NPay.Shared.Mediator;
using System.Reflection;
namespace NPay.Modules.Wallets.Application;

public static class Extensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient<IWalletsModuleApi, WalletsModuleApi>();
        services.AddMediator(Assembly.GetExecutingAssembly());
        return services;
    }
}