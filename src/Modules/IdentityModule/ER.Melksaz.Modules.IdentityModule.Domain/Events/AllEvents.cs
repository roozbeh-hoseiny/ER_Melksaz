using ER.Melksaz.BuildingBlocks.Domain.Core.Events;
using ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;
using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;

namespace ER.Melksaz.Modules.IdentityModule.Domain.Events;

public sealed record UserCreatedEvent(string Id) : DomainEventBase(DateTimeOffset.Now);
public sealed record ReferencedUserCreatedEvent(User SourceObject) : ReferencedEntityEvent<User, UserId, string, UserCreatedEvent>(SourceObject);


public sealed record UserFirstNameChangedEvent(string Id) : DomainEventBase(DateTimeOffset.Now);
public sealed record ReferencedUserFirstNameChangedEvent(User SourceObject) : ReferencedEntityEvent<User, UserId, string, UserFirstNameChangedEvent>(SourceObject);


public sealed record UserLastNameChangedEvent(string Id) : DomainEventBase(DateTimeOffset.Now);
public sealed record ReferencedUserLastNameChangedEvent(User SourceObject) : ReferencedEntityEvent<User, UserId, string, UserLastNameChangedEvent>(SourceObject);


public sealed record UserEmailChangedEvent(string Id) : DomainEventBase(DateTimeOffset.Now);
public sealed record ReferencedUserEmailChangedEvent(User SourceObject) : ReferencedEntityEvent<User, UserId, string, UserEmailChangedEvent>(SourceObject);


public sealed record UserMobileChangedEvent(string Id) : DomainEventBase(DateTimeOffset.Now);
public sealed record ReferencedUserMobileChangedEvent(User SourceObject) : ReferencedEntityEvent<User, UserId, string, UserMobileChangedEvent>(SourceObject);


public sealed record UserNationalCodeChangedEvent(string Id) : DomainEventBase(DateTimeOffset.Now);
public sealed record ReferencedUserNationalCodeChangedEvent(User SourceObject) : ReferencedEntityEvent<User, UserId, string, UserNationalCodeChangedEvent>(SourceObject);
