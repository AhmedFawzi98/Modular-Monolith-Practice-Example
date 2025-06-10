using System;
using MediatR;
using NPay.Modules.Wallets.Shared.DTO;

namespace NPay.Modules.Wallets.Application.Wallets.Queries;

public class GetWallet : IRequest<WalletDto>
{
    public Guid WalletId { get; set; }
}