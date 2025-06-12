using MassTransit;
using NPay.Modules.Notifications.Api.Services;
using NPay.Modules.Users.Shared.Events;
using System.Threading.Tasks;

namespace NPay.Modules.Notifications.Api.Handlers.Users;

public sealed class UserVerifiedNotificationHandler : IConsumer<UserVerified>
{
    private readonly IEmailSender _emailSender;

    public UserVerifiedNotificationHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task Consume(ConsumeContext<UserVerified> context)
    {
        var userVerifiedEvent = context.Message;
        await _emailSender.SendAsync(userVerifiedEvent.Email, "account_verified");
    }
}