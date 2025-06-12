using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using NPay.Modules.Notifications.Api.Services;
using NPay.Modules.Wallets.Shared.Events;
using NPay.Shared.Events;

namespace NPay.Modules.Notifications.Api.Handlers.Wallets;

internal sealed class FundsTransferredHandler : IConsumer<FundsTransferred>
{
    private readonly IEmailSender _emailSender;
    private readonly IEmailResolver _emailResolver;

    public FundsTransferredHandler(IEmailSender emailSender, IEmailResolver emailResolver)
    {
        _emailSender = emailSender;
        _emailResolver = emailResolver;
    }

    public async Task Consume(ConsumeContext<FundsTransferred> context)
    {
        var fundsTRansferredEvent = context.Message;
        await Task.WhenAll(_emailSender.SendAsync(_emailResolver.GetForOwner(fundsTRansferredEvent.FromWalletId), "funds_deducted"),
            _emailSender.SendAsync(_emailResolver.GetForOwner(fundsTRansferredEvent.ToWalletId), "funds_added"));
    }
}