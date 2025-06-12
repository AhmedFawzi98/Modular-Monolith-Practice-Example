using System.Threading.Tasks;
using System.Threading;

namespace NPay.Shared.Events;

public interface IEventPublisher
{
    Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
        where TDomainEvent : IDomainEvent;
    Task PublishIntegerationEventAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        where TIntegrationEvent : IIntegrationEvent;
}


