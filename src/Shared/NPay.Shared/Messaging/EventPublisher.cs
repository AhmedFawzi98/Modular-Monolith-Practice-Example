using MassTransit;
using NPay.Shared.Events;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

namespace NPay.Shared.Messaging;

public class EventPublisher : IEventPublisher
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public EventPublisher(IMediator mediator, IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
        where TDomainEvent : IDomainEvent
    {
        await _mediator.Publish(domainEvent, cancellationToken);
    }

    public async Task PublishIntegerationEventAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        where TIntegrationEvent : IIntegrationEvent
    {
        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}

