using NPay.Shared.Events;
using System;

namespace NPay.Modules.Users.Core.Domain;

internal record TrialDomainEventUserCreated(Guid UserId) : IDomainEvent;
