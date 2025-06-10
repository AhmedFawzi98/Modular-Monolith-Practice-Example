using System;
using MediatR;

namespace NPay.Modules.Wallets.Application.Wallets.Commands;

public record TransferFunds(Guid FromWalletId, Guid ToWalletId, decimal Amount) : IRequest;