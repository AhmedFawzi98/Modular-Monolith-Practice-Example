using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NPay.Modules.Wallets.Core.Wallets.Exceptions;
using NPay.Modules.Wallets.Core.Wallets.Repositories;
using NPay.Modules.Wallets.Shared.Events;
using NPay.Shared.Events;
using NPay.Shared.Messaging;
using NPay.Shared.Time;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NPay.Modules.Wallets.Application.Wallets.Commands.Handlers;

internal sealed class TransferFundsHandler : IRequestHandler<TransferFunds>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IClock _clock;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<TransferFundsHandler> _logger;

    public TransferFundsHandler(IWalletRepository walletRepository, IClock clock, IEventPublisher eventPublisher,
        ILogger<TransferFundsHandler> logger)
    {
        _walletRepository = walletRepository;
        _clock = clock;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task Handle(TransferFunds command, CancellationToken cancellationToken)
    {
        var (fromWalletId, toWalletId, amount) = command;
        var fromWallet = await _walletRepository.GetAsync(fromWalletId);
        if (fromWallet is null)
        {
            throw new WalletNotFoundException(fromWalletId);
        }

        var toWallet = await _walletRepository.GetAsync(toWalletId);
        if (toWallet is null)
        {
            throw new WalletNotFoundException(toWalletId);
        }

        var now = _clock.CurrentDate();
        var currency = fromWallet.Currency;
        fromWallet.TransferFunds(toWallet, amount, now);
        await _eventPublisher.PublishIntegerationEventAsync(new FundsTransferred(fromWalletId, toWalletId, amount, currency), cancellationToken);

        await _walletRepository.UpdateAsync(fromWallet);
        
        await _walletRepository.UpdateAsync(toWallet);
            
        _logger.LogInformation($"Transferred {amount} {currency} from: '{fromWallet.Id}' to: '{toWallet.Id}'.");
    }
}