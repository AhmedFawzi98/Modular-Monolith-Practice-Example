using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NPay.Shared.Database;

internal sealed class DbContextAppInitializer : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<DbContextAppInitializer> _logger;

    public DbContextAppInitializer(IServiceScopeFactory serviceScopeFactory, ILogger<DbContextAppInitializer> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }
        
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // apply peneding migrations (schema) of each module based on its DbContext
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(DbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(DbContext));

        using var scope = _serviceScopeFactory.CreateScope();
        foreach (var dbContextType in dbContextTypes)
        {
            var dbContext = scope.ServiceProvider.GetService(dbContextType) as DbContext;
            if (dbContext is null)
            {
                continue;
            }
                
            _logger.LogInformation($"Running DB context: {dbContext.GetType().Name}...");
            await dbContext.Database.MigrateAsync(cancellationToken);
        }

        //seed data using each module seeder
        var seeders = scope.ServiceProvider.GetServices<IDbSeeder>();
        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}