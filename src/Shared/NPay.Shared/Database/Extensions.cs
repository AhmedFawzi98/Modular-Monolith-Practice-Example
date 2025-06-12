using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NPay.Shared.Database;

public static class Extensions
{
    private const string SectionName = "postgres";
        
    internal static IServiceCollection AddPostgresOptionsAndDBInitalizer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetSection(SectionName));
        services.AddHostedService<DbContextAppInitializer>();
                        
        // Temporary fix for EF Core issue related to https://github.com/npgsql/efcore.pg/issues/2000
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }

    public static IServiceCollection AddPostgres<T>(this IServiceCollection services, string schemaName) where T : DbContext
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration[$"{SectionName}:{nameof(PostgresOptions.ConnectionString)}"];
        services.AddDbContext<T>(x => x.UseNpgsql(connectionString, npgsqlOptions =>
        {
            npgsqlOptions.MigrationsHistoryTable(DbConstants.MigraionHistoryTable, schemaName);
        }));

        return services;
    }
}