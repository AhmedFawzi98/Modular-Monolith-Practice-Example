using System;
using MediatR;

namespace NPay.Modules.Wallets.Application.Wallets.Commands;

public record AddFunds(Guid WalletId, decimal Amount) : IRequest;