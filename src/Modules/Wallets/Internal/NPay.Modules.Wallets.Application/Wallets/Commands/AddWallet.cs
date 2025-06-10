using System;
using MediatR;

namespace NPay.Modules.Wallets.Application.Wallets.Commands;

public record AddWallet(Guid OwnerId, string Currency) : IRequest
{
    public Guid WalletId { get; init; } = Guid.NewGuid();
}