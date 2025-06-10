using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace NPay.Shared.Mediator;

public static class Extensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly applicationAssembly)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly));

        return services;
    }
}