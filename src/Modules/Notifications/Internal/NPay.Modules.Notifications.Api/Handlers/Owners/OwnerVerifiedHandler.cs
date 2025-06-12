using MassTransit;
using NPay.Modules.Notifications.Api.Services;
using NPay.Modules.Wallets.Shared.Events;
using System.Threading.Tasks;

namespace NPay.Modules.Notifications.Api.Handlers.Owners;

public sealed class OwnerVerifiedNotificationHandler : IConsumer<OwnerVerified>
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailResolver _emailResolver;

    public OwnerVerifiedNotificationHandler(IEmailSender emailSender, IEmailResolver emailResolver)
    {
        _emailSender = emailSender;
        _emailResolver = emailResolver;
    }

    public async Task Consume(ConsumeContext<OwnerVerified> context)
    {
        var ownerVerifiedEvent = context.Message;
        await _emailSender.SendAsync(_emailResolver.GetForOwner(ownerVerifiedEvent.OwnerId), "owner_verified");
    }

}