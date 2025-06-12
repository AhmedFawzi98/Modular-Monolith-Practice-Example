using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using NPay.Modules.Users.Shared.Events;
using NPay.Modules.Wallets.Core.Owners.Exceptions;
using NPay.Modules.Wallets.Core.Owners.Repositories;
using NPay.Modules.Wallets.Shared.Events;
using NPay.Shared.Events;
using NPay.Shared.Time;

namespace NPay.Modules.Wallets.Infrastructure.ExternalEvents.Owners.Handlers;

public sealed class UserVerifiedOwnersHandler : IConsumer<UserVerified>
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IClock _clock;
    private readonly ILogger<UserVerifiedOwnersHandler> _logger;

    public UserVerifiedOwnersHandler(IOwnerRepository ownerRepository, IEventPublisher eventPublisher, IClock clock,
        ILogger<UserVerifiedOwnersHandler> logger)
    {
        _ownerRepository = ownerRepository;
        _eventPublisher = eventPublisher;
        _clock = clock;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserVerified> context)
    {
        var userVerifiedEvent = context.Message;

        var owner = await _ownerRepository.GetAsync(userVerifiedEvent.UserId);
        if (owner is null)
        {
            throw new OwnerNotFoundException(userVerifiedEvent.UserId);
        }

        var now = _clock.CurrentDate();
        owner.Verify(now);
        await _ownerRepository.UpdateAsync(owner);

        await _eventPublisher.PublishIntegerationEventAsync(new OwnerVerified(owner.Id));

        _logger.LogInformation($"Verified an owner for the user with ID: '{userVerifiedEvent.UserId}'.");
    }
}