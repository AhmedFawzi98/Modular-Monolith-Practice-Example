using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using NPay.Modules.Notifications.Api.Services;
using NPay.Modules.Wallets.Shared.Events;
using NPay.Shared.Events;

namespace NPay.Modules.Notifications.Api.Handlers.Wallets;

internal sealed class FundsAddedHandler : IConsumer<FundsAdded>
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailResolver _emailResolver;

    public FundsAddedHandler(IEmailSender emailSender, IEmailResolver emailResolver)
    {
        _emailSender = emailSender;
        _emailResolver = emailResolver;
    }

    public async Task Consume(ConsumeContext<FundsAdded> context)
    {
        var fundsAddedEvent = context.Message;
        await _emailSender.SendAsync(_emailResolver.GetForOwner(fundsAddedEvent.OwnerId), "funds_added"); 
    }
}