using MassTransit;
using NPay.Modules.Notifications.Api.Services;
using NPay.Modules.Users.Shared.Events;
using System.Threading.Tasks;

namespace NPay.Modules.Notifications.Api.Handlers.Users;

public sealed class UserCreatedNotificaionHandler : IConsumer<UserCreated>
{
    private readonly IEmailSender _emailSender;

    public UserCreatedNotificaionHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task Consume(ConsumeContext<UserCreated> context)
    {
        var userCreatedEvent = context.Message;
        await _emailSender.SendAsync(userCreatedEvent.Email, "account_created");
    }
        
}