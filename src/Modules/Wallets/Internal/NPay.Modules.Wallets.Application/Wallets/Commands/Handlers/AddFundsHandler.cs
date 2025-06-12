using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NPay.Modules.Wallets.Core.Wallets.Exceptions;
using NPay.Modules.Wallets.Core.Wallets.Repositories;
using NPay.Modules.Wallets.Core.Wallets.ValueObjects;
using NPay.Modules.Wallets.Shared.Events;
using NPay.Shared.Events;
using NPay.Shared.Time;

namespace NPay.Modules.Wallets.Application.Wallets.Commands.Handlers;

internal sealed class AddFundsHandler : IRequestHandler<AddFunds>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IClock _clock;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<AddFundsHandler> _logger;

    public AddFundsHandler(IWalletRepository walletRepository, IClock clock, IEventPublisher eventPublisher,
        ILogger<AddFundsHandler> logger)

    {
        _walletRepository = walletRepository;
        _clock = clock;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task Handle(AddFunds command, CancellationToken cancellationToken)
    {
        var (walletId, amount) = command;
        var wallet = await _walletRepository.GetAsync(walletId);
        if (wallet is null)
        {
            throw new WalletNotFoundException(walletId);
        }

        var now = _clock.CurrentDate();
        var transfer = wallet.AddFunds(new TransferId(), amount, now);

        await _eventPublisher.PublishIntegerationEventAsync(new FundsAdded(walletId, wallet.OwnerId, transfer.Currency, transfer.Amount), cancellationToken);

        await _walletRepository.UpdateAsync(wallet);
            
        _logger.LogInformation($"Added {transfer.Amount} {transfer.Currency} to the wallet: '{wallet.Id}'");
    }
}