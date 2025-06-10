using System;
using System.Threading.Tasks;
using MediatR;
using NPay.Modules.Wallets.Application.Wallets.Queries;
using NPay.Modules.Wallets.Shared;
using NPay.Modules.Wallets.Shared.DTO;
using NPay.Shared.Events;

namespace NPay.Modules.Wallets.Application.Services;

internal sealed class WalletsModuleApi : IWalletsModuleApi
{
    private readonly IMediator _mediator;

    public WalletsModuleApi(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<WalletDto> GetWalletAsync(Guid walletId)
        => await _mediator.Send(new GetWallet { WalletId = walletId });
}