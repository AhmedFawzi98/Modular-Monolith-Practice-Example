using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace NPay.Modules.Users.Core.Domain;

internal class TrialDomainEventUserCreatedHandler : INotificationHandler<TrialDomainEventUserCreated>
{
    private readonly ILogger<TrialDomainEventUserCreatedHandler> _logger;

    public TrialDomainEventUserCreatedHandler(ILogger<TrialDomainEventUserCreatedHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TrialDomainEventUserCreated notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handled domain event for user {notification.UserId}");

        return Task.CompletedTask;
    }
}
