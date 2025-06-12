using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using NPay.Modules.Notifications.Api.Services;
using NPay.Modules.Wallets.Shared.Events;
using NPay.Shared.Events;

namespace NPay.Modules.Notifications.Api.Handlers.Wallets;

internal sealed class WalletAddedHandler : IConsumer<WalletAdded>
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailResolver _emailResolver;

    public WalletAddedHandler(IEmailSender emailSender, IEmailResolver emailResolver)
    {
        _emailSender = emailSender;
        _emailResolver = emailResolver;
    }

    public async Task Consume(ConsumeContext<WalletAdded> context)
    {
        var walletAddedEvent = context.Message;
        await _emailSender.SendAsync(_emailResolver.GetForOwner(walletAddedEvent.OwnerId), "wallet_added");
    }
}