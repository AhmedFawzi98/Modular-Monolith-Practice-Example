using MediatR;
using NPay.Modules.Wallets.Application.Wallets.Storage;
using NPay.Modules.Wallets.Shared.DTO;
using System.Threading;
using System.Threading.Tasks;

namespace NPay.Modules.Wallets.Application.Wallets.Queries.Handlers;

internal sealed class GetWalletHandler : IRequestHandler<GetWallet, WalletDto>
{
    private readonly IWalletStorage _storage;

    public GetWalletHandler(IWalletStorage storage)
    {
        _storage = storage;
    }

    public async Task<WalletDto> Handle(GetWallet query, CancellationToken cancellationToken)
    {
        var wallet = await _storage.FindAsync(x => x.Id == query.WalletId);

        return wallet?.AsDto();
    }
}