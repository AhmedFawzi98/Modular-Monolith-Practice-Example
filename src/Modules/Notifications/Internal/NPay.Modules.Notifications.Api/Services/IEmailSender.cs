using System.Threading.Tasks;

namespace NPay.Modules.Notifications.Api.Services;

public interface IEmailSender
{
    Task SendAsync(string receiver, string template);
}