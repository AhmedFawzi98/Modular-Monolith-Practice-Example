﻿using Microsoft.Extensions.DependencyInjection;
using NPay.Modules.Wallets.Application.Wallets.Storage;
using NPay.Modules.Wallets.Core.Owners.Repositories;
using NPay.Modules.Wallets.Core.Wallets.Repositories;
using NPay.Modules.Wallets.Infrastructure.DAL;
using NPay.Modules.Wallets.Infrastructure.DAL.Constants;
using NPay.Modules.Wallets.Infrastructure.DAL.Repositories;
using NPay.Modules.Wallets.Infrastructure.DAL.Seeder;
using NPay.Modules.Wallets.Infrastructure.Storage;
using NPay.Shared.Database;

namespace NPay.Modules.Wallets.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddPostgres<WalletsDbContext>(WalletDbConstants.SchemaName);
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<IWalletStorage, WalletStorage>();

        services.AddScoped<IDbSeeder, WalletsSeeder>();
            
        return services;
    }
}