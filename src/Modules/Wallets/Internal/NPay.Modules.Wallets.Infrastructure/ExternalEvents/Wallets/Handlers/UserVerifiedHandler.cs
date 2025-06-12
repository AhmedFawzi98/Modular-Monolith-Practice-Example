using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using NPay.Modules.Users.Shared.Events;
using NPay.Modules.Wallets.Core.Wallets.Aggregates;
using NPay.Modules.Wallets.Core.Wallets.Repositories;
using NPay.Modules.Wallets.Core.Wallets.Services;
using NPay.Modules.Wallets.Shared.Events;
using NPay.Shared.Events;
using NPay.Shared.Messaging;
using NPay.Shared.Time;

namespace NPay.Modules.Wallets.Infrastructure.ExternalEvents.Wallets.Handlers;

public sealed class UserVerifiedWalletsHandler : IConsumer<UserVerified>
{
    private readonly IWalletRepository _walletRepository;
    private readonly ICurrencyResolver _currencyResolver;
    private readonly IClock _clock;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<UserVerifiedWalletsHandler> _logger;

    public UserVerifiedWalletsHandler(IWalletRepository walletRepository, ICurrencyResolver currencyResolver,
        IClock clock, IEventPublisher eventPublisher, ILogger<UserVerifiedWalletsHandler> logger)
    {
        _walletRepository = walletRepository;
        _currencyResolver = currencyResolver;
        _clock = clock;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserVerified> context)
    {
        var userVerifiedEvent = context.Message;
        var now = _clock.CurrentDate();
        var currency = _currencyResolver.Resolve(userVerifiedEvent.Nationality);
        var wallet = Wallet.Create(userVerifiedEvent.UserId, currency, now);

        _walletRepository.Add(wallet);

        await _eventPublisher.PublishIntegerationEventAsync(new WalletAdded(wallet.Id, wallet.OwnerId, wallet.Currency));

        _logger.LogInformation($"Added the wallet with ID: '{wallet.Id}' for an owner: '{wallet.OwnerId}'.");
    }
}