using System.Threading;
using System.Threading.Tasks;

namespace NPay.Shared.Database;

public interface IDbSeeder
{
    Task SeedAsync();
}
