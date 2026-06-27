using ER.Melksaz.BuildingBlocks.Domain.Core;
using ER.Melksaz.Modules.IdentityModule.Domain.Events;
using ER.Melksaz.Modules.IdentityModule.Domain.ValueObjects;
using ER.Melksaz.PrimitiveResults;

namespace ER.Melksaz.Modules.IdentityModule.Domain.Aggregates.UserAggregate;

public sealed class User : DomainAggregateRootBase<UserId>
{
    #region " Properties "
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public NationalCode NationalCode { get; private set; }
    public Mobile Mobile { get; private set; }
    public Email Email { get; private set; }
    public Username Username { get; private set; }
    public PasswordHash Password { get; private set; }
    public bool IsActive { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? LastModifiedAt { get; private set; }
    #endregion

    private User() : base(UserId.Empty) { }         // EF
    private User(
        UserId id,
        FirstName firstName,
        LastName lastName,
        NationalCode nationalCode,
        Mobile mobile,
        Email email,
        Username username,
        PasswordHash password) : base(id)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.NationalCode = nationalCode;
        this.Mobile = mobile;
        this.Email = email;
        this.Username = username;
        this.Password = password;
        this.IsActive = true;
        this.CreatedAt = DateTimeOffset.Now;
    }

    public static PrimitiveResult<User> Create(
        FirstName firstName,
        LastName lastName,
        NationalCode nationalCode,
        Mobile mobile,
        Email email,
        Username username,
        PasswordHash password)
    {
        var result = new User(
            UserId.Create(),
            firstName,
            lastName,
            nationalCode,
            mobile,
            email,
            username,
            password);

        result.AddDomainEvent(new ReferencedUserCreatedEvent(result));

        return result;
    }

    public PrimitiveResult ChangeFirstName(FirstName value)
    {
        if (this.FirstName == value)
            return PrimitiveResult.Success();

        this.FirstName = value;

        this.Touch();

        this.AddDomainEvent(new ReferencedUserFirstNameChangedEvent(this));

        return PrimitiveResult.Success();
    }

    public PrimitiveResult ChangeLastName(LastName value)
    {
        if (this.LastName == value)
            return PrimitiveResult.Success();

        this.LastName = value;

        this.Touch();

        this.AddDomainEvent(new ReferencedUserLastNameChangedEvent(this));

        return PrimitiveResult.Success();
    }

    public PrimitiveResult ChangeNationalCode(NationalCode value)
    {
        if (this.NationalCode == value)
            return PrimitiveResult.Success();

        this.NationalCode = value;

        this.Touch();

        this.AddDomainEvent(new ReferencedUserNationalCodeChangedEvent(this));

        return PrimitiveResult.Success();
    }

    public PrimitiveResult ChangeMobile(Mobile value)
    {
        if (this.Mobile == value)
            return PrimitiveResult.Success();

        this.Mobile = value;

        this.Touch();

        this.AddDomainEvent(new ReferencedUserMobileChangedEvent(this));

        return PrimitiveResult.Success();
    }

    public PrimitiveResult ChangeEmail(Email value)
    {
        if (this.Email == value)
            return PrimitiveResult.Success();

        this.Email = value;

        this.Touch();

        this.AddDomainEvent(new ReferencedUserEmailChangedEvent(this));

        return PrimitiveResult.Success();
    }


    private void Touch()
    {
        this.LastModifiedAt = DateTime.UtcNow;
    }
}
