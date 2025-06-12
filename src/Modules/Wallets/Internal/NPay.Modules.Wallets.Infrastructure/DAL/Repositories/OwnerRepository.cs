using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NPay.Modules.Wallets.Core.Owners.Aggregates;
using NPay.Modules.Wallets.Core.Owners.Repositories;
using NPay.Modules.Wallets.Core.SharedKernel;

namespace NPay.Modules.Wallets.Infrastructure.DAL.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly WalletsDbContext _context;
    private readonly DbSet<Owner> _owners;

    public OwnerRepository(WalletsDbContext context)
    {
        _context = context;
        _owners = _context.Owners;
    }

    public Task<Owner> GetAsync(OwnerId id)
        => _owners.SingleOrDefaultAsync(x => x.Id.Equals(id));

    public async Task Add (Owner owner)
    {
        _owners.Add(owner);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Owner owner)
    {
        _owners.Update(owner);
        await _context.SaveChangesAsync();
    }
}