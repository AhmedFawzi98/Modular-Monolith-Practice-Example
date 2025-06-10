using NPay.Shared.Database;
using System;
using System.Threading.Tasks;

namespace NPay.Modules.Users.Core.DAL.Seeder;

internal class UsersSeeder : IDbSeeder
{
    private readonly UsersDbContext _usersDbContext;
    public UsersSeeder(UsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }

    public async Task SeedAsync()
    {
        // to do
        Console.WriteLine("seeding users");

    }
}

