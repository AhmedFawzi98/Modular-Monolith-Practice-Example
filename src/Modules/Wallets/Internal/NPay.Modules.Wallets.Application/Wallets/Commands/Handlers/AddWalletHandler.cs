using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NPay.Modules.Wallets.Core.Wallets.Aggregates;
using NPay.Modules.Wallets.Core.Wallets.Repositories;
using NPay.Modules.Wallets.Shared.Events;
using NPay.Shared.Events;
using NPay.Shared.Messaging;
using NPay.Shared.Time;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NPay.Modules.Wallets.Application.Wallets.Commands.Handlers;

internal sealed class AddWalletHandler : IRequestHandler<AddWallet>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IClock _clock;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<AddWalletHandler> _logger;

    public AddWalletHandler(IWalletRepository walletRepository, IClock clock, IEventPublisher eventPublisher,
        ILogger<AddWalletHandler> logger)
    {
        _walletRepository = walletRepository;
        _clock = clock;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task Handle(AddWallet command, CancellationToken cancellationToken)
    {
        var now = _clock.CurrentDate();
        var wallet = Wallet.Create(command.WalletId, command.OwnerId, command.Currency, now);

        await _eventPublisher.PublishIntegerationEventAsync(new WalletAdded(wallet.Id, wallet.OwnerId, wallet.Currency), cancellationToken);
        
        await _walletRepository.AddAsyncAndSave(wallet);
        _logger.LogInformation($"Added the wallet with ID: '{wallet.Id}' for an owner: '{wallet.OwnerId}'.");
    }
}