using System;

namespace NPay.Modules.Notifications.Api.Services;

public interface IEmailResolver
{
    string GetForOwner(Guid ownerId);
}