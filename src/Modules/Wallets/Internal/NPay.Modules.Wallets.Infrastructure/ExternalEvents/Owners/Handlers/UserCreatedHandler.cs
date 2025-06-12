using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using NPay.Modules.Users.Shared.Events;
using NPay.Modules.Wallets.Application.Owners.Exceptions;
using NPay.Modules.Wallets.Core.Owners.Aggregates;
using NPay.Modules.Wallets.Core.Owners.Repositories;
using NPay.Shared.Events;
using NPay.Shared.Time;

namespace NPay.Modules.Wallets.Infrastructure.ExternalEvents.Owners.Handlers;

public sealed class UserCreatedOwnersHandler : IConsumer<UserCreated>
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly IClock _clock;
    private readonly ILogger<UserCreatedOwnersHandler> _logger;

    public UserCreatedOwnersHandler(IOwnerRepository ownerRepository, IClock clock, ILogger<UserCreatedOwnersHandler> logger)
    {
        _ownerRepository = ownerRepository;
        _clock = clock;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        var userCreatedEvent = context.Message;
        if (await _ownerRepository.GetAsync(userCreatedEvent.UserId) is not null)
        {
            throw new OwnerAlreadyExistsException(userCreatedEvent.Email);
        }

        var now = _clock.CurrentDate();
        var owner = new Owner(userCreatedEvent.UserId, userCreatedEvent.FullName, userCreatedEvent.Nationality, now);

        _ownerRepository.Add(owner);


        _logger.LogInformation($"Created an owner for the user with ID: '{userCreatedEvent.UserId}'.");
    }
}