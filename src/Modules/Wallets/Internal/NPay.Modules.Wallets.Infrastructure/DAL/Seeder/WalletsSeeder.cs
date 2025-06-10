using Microsoft.EntityFrameworkCore;
using NPay.Modules.Wallets.Core.Owners.Aggregates;
using NPay.Modules.Wallets.Core.Wallets.Aggregates;
using NPay.Modules.Wallets.Core.Wallets.Entities;
using NPay.Shared.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPay.Modules.Wallets.Infrastructure.DAL.Seeder;

internal class WalletsSeeder : IDbSeeder
{
    private readonly WalletsDbContext _walletsDbContext;
    public WalletsSeeder(WalletsDbContext walletsDbContext)
    {
        _walletsDbContext = walletsDbContext;
    }

    public async Task SeedAsync()
    {
        //to do
        Console.WriteLine("seeding wallets");
    }
}
