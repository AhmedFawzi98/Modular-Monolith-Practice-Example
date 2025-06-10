using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NPay.Modules.Users.Shared;
using NPay.Modules.Wallets.Application.Owners.Exceptions;
using NPay.Modules.Wallets.Core.Owners.Aggregates;
using NPay.Modules.Wallets.Core.Owners.Repositories;
using NPay.Shared.Time;

namespace NPay.Modules.Wallets.Application.Owners.Commands.Handlers;

internal sealed class AddOwnerHandler : IRequestHandler<AddOwner>
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly IUsersModuleApi _usersModuleApi;
    private readonly IClock _clock;
    private readonly ILogger<AddOwnerHandler> _logger;

    public AddOwnerHandler(IOwnerRepository ownerRepository, IUsersModuleApi usersModuleApi, IClock clock,
        ILogger<AddOwnerHandler> logger)
    {
        _ownerRepository = ownerRepository;
        _usersModuleApi = usersModuleApi;
        _clock = clock;
        _logger = logger;
    }

    public async Task Handle(AddOwner command, CancellationToken cancellationToken)
    {
        var user = await _usersModuleApi.GetUserAsync(command.Email);
        if (user is null)
        {
            throw new UserNotFoundException(command.Email);
        }

        if (await _ownerRepository.GetAsync(user.UserId) is not null)
        {
            throw new OwnerAlreadyExistsException(command.Email);
        }

        var now = _clock.CurrentDate();
        var owner = new Owner(user.UserId, user.FullName, user.Nationality, now);
        await _ownerRepository.AddAsync(owner);
        _logger.LogInformation($"Created an owner for the user with ID: '{user.UserId}'.");
    }
}