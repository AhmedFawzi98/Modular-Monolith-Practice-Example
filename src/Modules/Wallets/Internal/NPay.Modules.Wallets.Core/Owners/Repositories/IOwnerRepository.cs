using System.Threading.Tasks;
using NPay.Modules.Wallets.Core.Owners.Aggregates;
using NPay.Modules.Wallets.Core.SharedKernel;

namespace NPay.Modules.Wallets.Core.Owners.Repositories;

public interface IOwnerRepository
{
    Task<Owner> GetAsync(OwnerId id);
    Task Add (Owner owner);
    Task UpdateAsync(Owner owner);
}